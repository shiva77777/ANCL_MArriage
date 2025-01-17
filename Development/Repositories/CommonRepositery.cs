using ANCL_Marriage_MVC.Data;
using ANCL_Marriage_MVC.Models;
using ANCL_Marriage_MVC.Repositories.Interface;
using Dapper;
using FastMember;
using System.Data;

namespace ANCL_Marriage_MVC.Repositories
{
    public class CommonRepositery: ICommonRepositery
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CommonRepositery(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<ULBS>> GetULBS()
        {
            using var connection = applicationDbContext.CreateConnection();
            IEnumerable<ULBS> ulbs;

            ulbs = await connection.QueryAsync<ULBS>(Queries.vw_ulbs);
            return ulbs;
        }

        public async Task<ULBS> GetULBSById(string ulbId)
        {
            using var connection = applicationDbContext.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<ULBS>(Queries.vw_ulbsbyid, new { id = ulbId }) ?? new ULBS();

        }


        public async Task<List<Menu>> GetMenusAsync(int deptId, string userId, Int64? ulbId)
        {
            string query = @"
            SELECT num_menumaster_menuid AS MenuId, 
                   var_menumaster_pagetitle AS MenuTitle, 
                   num_menumaster_parentmenuid AS ParentId, 
                   REPLACE(var_menumaster_pagepath, '.aspx', '') AS PagePath, 
                   num_menumaster_orderby AS OrderBy
            FROM admins.aoma_menumaster_mas
            WHERE num_menumaster_parentmenuid = 0 
              AND num_menumaster_deptid = :DeptId
            UNION
            SELECT num_menumaster_menuid AS MenuId, 
                   var_menumaster_pagetitle AS MenuTitle, 
                   num_menumaster_parentmenuid AS ParentId, 
                   REPLACE(var_menumaster_pagepath, '.aspx', '') AS PagePath, 
                   num_menumaster_orderby AS OrderBy
            FROM admins.aoma_menumaster_mas
            INNER JOIN admins.aoma_MenuULB_Config 
                ON num_menucorporation_menuid = num_menumaster_menuid
            INNER JOIN admins.aoma_MenuUser_Config 
                ON num_menuuser_menuid = num_menumaster_menuid
            WHERE var_menuuser_activeflag = 'Y' 
              AND var_menuuser_userid = :UserId 
              AND var_menumaster_pagepath IS NOT NULL
              AND num_menumaster_deptid = :DeptId
              AND num_menucorporation_ulbid = :UlbId
            ORDER BY OrderBy";
            using var connection = applicationDbContext.CreateConnection();

            var menus = await connection.QueryAsync<Menu>(query, new
            {
                DeptId = deptId,
                UserId = userId,
                UlbId = ulbId
            });
            return menus.ToList();

        }

        public async Task<IEnumerable<DropDown>> BindDropDown(String Query)
        {
            using var connection = applicationDbContext.CreateConnection();
            IEnumerable<DropDown> data;
                
            data = await connection.QueryAsync<DropDown>(Query);
            return data;
        }

        public async Task<DataTable> ExecuteQueryAsyncfastmember(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Open a connection to the database
                using var connection = applicationDbContext.CreateConnection();
               

                // Execute the query with parameters
                var result = await connection.QueryAsync(query);

                // Convert the result to DataTable
                if (result != null)
                {
                    using (var reader = ObjectReader.Create(result))
                    {
                        dataTable.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle it as per your application's requirements
                throw new Exception("Error executing query", ex);
            }

            return dataTable;
        }

        public async Task<DataTable> ExecuteQueryAsync(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using var connection = applicationDbContext.CreateConnection();             

                // Execute the query with parameters
                var result = await connection.QueryAsync(query);

                // Build DataTable columns
                if (result != null)
                {
                    var firstRow = result.FirstOrDefault();
                    if (firstRow != null)
                    {
                        foreach (var property in ((IDictionary<string, object>)firstRow).Keys)
                        {
                            dataTable.Columns.Add(property);
                        }

                        // Populate rows
                        foreach (var row in result)
                        {
                            var dataRow = dataTable.NewRow();
                            foreach (var property in ((IDictionary<string, object>)row))
                            {
                                dataRow[property.Key] = property.Value ?? DBNull.Value;
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing query", ex);
            }

            return dataTable;
        }

    }
}
