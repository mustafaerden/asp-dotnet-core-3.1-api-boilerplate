using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Kurumsal.Core.Aspects.Autofac.Logging;
using Kurumsal.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Kurumsal.Core.Utilities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        // Eger product harici tablolar ile burada bir islem yapilacaksa onu servis haline getirmeliyiz;
        // Yani controllerdan calisir gibi service i cagiricaz;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        // Priority ile 1 ise once o calisir, 2, 3, 4 sirali calisir
        // Create islemi basarili oldugunda Cache islemi yapilmissa silinmesi icin CacheRemoveAspect i calistiriyoruz;
        // IProductService te Get ismi ile baslayan metotlardaki Cache silecek!
        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            // Validation islemini aspect ile yapiyoruz ki yukarda [Validation] seklinde yazabilelim;
            //ValidationTool.Validate(new ProductValidator(), product);
            // Bunun Calismasi Icin DependencyResolvers / Autofac / AutofacBusinessModule icinde tanimlamalar yaptik!

            // Mesela product name lerin uniqu olmasini istiyoruz. ayni product ismi ile bir urun daha eklenmemeli;
            // IResult result = BusinessRules.Run(CheckIfProductExists(product.Name), CheckIfCategoryIsEnabled());
            IResult result = BusinessRules.Run(CheckIfProductExists(product.Name));

            //IResult result = CheckIfProductExists(product.Name);

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductCreatedMessage);
        }

        #region #HelperMethods(Business Rules)

        private IResult CheckIfProductExists(string name)
        {
            var result = _productDal.GetList(p => p.Name == name).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductAlReadyExistsMessage);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryIsEnabled()
        {
            var result = _categoryService.GetList();
            if (result.Data.Count < 10)
            {
                return new ErrorResult(Messages.CategoryCountNotEnough);
            }

            return new SuccessResult();
        }

        #endregion



        public IResult Delete(Product product)
        {
            _productDal.Delete(product);

            return new SuccessResult(Messages.ProductDeletedMessage);
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == id));
        }

        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetList()
        {
            // istek 5 saniyeyi geciyomu testi icin;
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        //[SecuredOperation("Product.List,Admin")]
        [CacheAspect(duration:10)]
        // File a loglari yazdirmak icin FileLogger classini cagir;
        // [LogAspect(typeof(FileLogger))]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        // Transaction Testi Icin;
        //[TransactionScopeAspect]
        //public IResult TransactionOperation(Product product)
        //{
        //    _productDal.Update(product);
        //    _productDal.Add(product);

        //    return new SuccessResult(Messages.ProductUpdatedMessage);
        //}

        public IResult Update(Product product)
        {
            _productDal.Update(product);

            return new SuccessResult(Messages.ProductUpdatedMessage);
        }
    }
}
