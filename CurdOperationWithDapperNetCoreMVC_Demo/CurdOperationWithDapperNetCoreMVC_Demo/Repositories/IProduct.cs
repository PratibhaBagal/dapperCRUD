using CurdOperationWithDapperNetCoreMVC_Demo.Models;

namespace CurdOperationWithDapperNetCoreMVC_Demo.Repositories
{
    public interface IProduct
    {



        Task<IEnumerable<ProductsModel>> Get();
        Task<ProductsModel> Find(Guid uid);
        Task<ProductsModel> Add(ProductsModel model);
        Task<ProductsModel> Update(ProductsModel model);
        Task<ProductsModel> Remove(ProductsModel model);

    }
}
