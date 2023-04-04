using ManojHiray_Nimap.Config;
using ManojHiray_Nimap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ManojHiray_Nimap.Controllers
{
    public class ProductlistController : Controller
    {
        private readonly ILogger<ProductlistController> _logger;

        public ProductlistController(ILogger<ProductlistController> logger)
        {
            _logger = logger;
        }

        public IActionResult ProductList(int pageNumber = 1, int pageSize = 10)
        {
            List<Product> productobj = GetData("[SP_Product_list]", pageNumber, pageSize);
            int count=0;

            for (int i = 0; i <= productobj.Count; i++)
            {
                 count = i;
            }
            var viewModel = new PaginationRecords
            {
                Records = productobj,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Count = count


            };
            return View(viewModel);
           
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public List<Product> GetData(string storedProcedure, int pageNumber, int pageSize)
        {
            List<Product> productobj = new List<Product>();

            using (SqlConnection conn = new SqlConnection(StoreConnection.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_Product_list", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (conn.State != System.Data.ConnectionState.Open) ;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Action", "Select");
                    var pageNumberParam = new SqlParameter("@PageNumber", SqlDbType.Int);
                    pageNumberParam.Value = pageNumber;
                    cmd.Parameters.Add(pageNumberParam);

                    var pageSizeParam = new SqlParameter("@PageSize", SqlDbType.Int);
                    pageSizeParam.Value = pageSize;
                    cmd.Parameters.Add(pageSizeParam);

                    SqlDataReader sDReader = cmd.ExecuteReader();
                    DataTable dTable = new DataTable();
                    dTable.Load(sDReader);
                    //cmd.Parameters.AddWithValue("@Query", 1);

                    foreach (DataRow row in dTable.Rows)
                    {
                        productobj.Add(
                            new Product
                            {
                                ProductId = Convert.ToInt32(row["ProductId"]),
                                ProductName = row["ProductName"].ToString(),
                                CategoryId = Convert.ToInt32(row["CategoryId"]),
                                CategoryName = row["CategoryName"].ToString(),
                              


                            });
                    }

                }
            }

            return productobj;

          

            //return viewModel;
        }
    }

}
