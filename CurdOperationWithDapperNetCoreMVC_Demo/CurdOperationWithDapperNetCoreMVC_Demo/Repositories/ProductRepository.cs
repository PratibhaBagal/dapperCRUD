using CurdOperationWithDapperNetCoreMVC_Demo.DBContext;
using CurdOperationWithDapperNetCoreMVC_Demo.Models;
using Dapper;

namespace CurdOperationWithDapperNetCoreMVC_Demo.Repositories
{
    public class ProductRepository:IProduct
    {
        private readonly DapperContext context;
        public ProductRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductsModel>>Get()
        {
            var sql = $@"SELECT [ProductId],
                               [ProductName],
                               [Price],
                               [ProductDescription],
                               [CreatedOn],
                               [UpdateOn]
                            FROM
                               [Products]";

            using var connection = context.CreateConnection();

             return await connection.QueryAsync<ProductsModel>(sql);

            

        }

        public async Task<ProductsModel> Find(Guid uid)
        {
            var sql = $@"SELECT [ProductId],
                               [ProductName],
                               [Price],
                               [ProductDescription],
                               [CreatedOn],
                               [UpdateOn]
                            FROM
                               [Products]
                            WHERE
                              [ProductId]=@uid";

            using var connection = context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ProductsModel>(sql, new { uid });
        }

        public async Task<ProductsModel> Add(ProductsModel model)
        {
            
            model.CreatedOn = DateTime.Now;
            var sql = $@"INSERT INTO [dbo].[Products]
                                ([ProductId],
                                 [ProductName],
                                 [Price],
                                 [ProductDescription],
                                 [CreatedOn])
                                VALUES
                                (@ProductId,
                                 @ProductName,
                                 @Price,
                                 @ProductDescription,
                                 @CreatedOn)";

            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<ProductsModel> Update(ProductsModel model)
        {
            model.UpdateOn = DateTime.Now;
            var sql = $@"UPDATE[dbo].[Products]
                           SET [ProductId] = @ProductId,
                               [ProductName] = @ProductName,
                               [Price] = @Price,
                               [ProductDescription] = @ProductDescription,
                               [UpdateOn] = @UpdateOn
                          WHERE
                              ProductId=@ProductId";

            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }


        public async Task<ProductsModel> Remove(ProductsModel model)
        {
            var sql = $@"
                        DELETE FROM
                            [dbo].[Products]
                        WHERE
                            [ProductId]=@ProductId";
            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

    }
}