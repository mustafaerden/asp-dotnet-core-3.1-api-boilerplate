using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<Category> GetById(int id);
        IDataResult<List<Category>> GetList();

        // void olan metotlari IResult donuyoruz;
        IResult Add(Category category);
        IResult Delete(Category category);
        IResult Update(Category category);
    }
}
