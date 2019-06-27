using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    class TafsilController:Repository.Repository
    {
        /// <summary>
        /// دریافت لیست موجودیت های سیستم
        /// </summary>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.EntityModelBinding> GetEntities()
        {
            using (var repo = new Repository.Repository(this,"usp_getEntityList",true))
            {
                repo.pageNo = 0;
                repo.seedNumber = 10;
                repo.ExecuteAdapter();
                var data = repo.ds.Tables[0].AsEnumerable();
                return new Models.SelectResultModelBinding<Models.EntityModelBinding>
                {
                    Body = data.Select(c=> new Models.EntityModelBinding
                    {
                        Id = Convert.ToInt32(c.Field<object>("id")),
                        Title = c.Field<object>("title").ToString()
                    }).ToList(),
                    TotalCount = repo.totalCount
                };
            }
        }
        /// <summary>
        /// ثبت حساب تفصیل جدید
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding InsertTafsil(Models.Tafsil model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_insertTafsilAccount"))
                {
                    repo.cmd.Parameters.AddWithValue("@code", model.Code);
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@automateDocumentTitle", model.TitleInAutomateDocument);
                    repo.cmd.Parameters.AddWithValue("@isActive", model.IsActive);
                    repo.cmd.Parameters.AddWithValue("@hasCostCenter", model.IsCostCenter);
                    repo.cmd.Parameters.AddWithValue("@fk_typ_entity_type", model.EntityTypeId);
                    repo.cmd.Parameters.AddWithValue("@tel", model.Tel);
                    repo.cmd.Parameters.AddWithValue("@mobile", model.Mobile);
                    repo.cmd.Parameters.AddWithValue("@address", model.Address);
                    repo.cmd.Parameters.AddWithValue("@nationalCode", model.NationalCode);
                    repo.cmd.Parameters.AddWithValue("@economyCode", model.EconomyCode);
                    repo.cmd.Parameters.AddWithValue("@accTypeId", model.AccountTypeId);
                    repo.cmd.Parameters.AddWithValue("@maxCredit", model.MaxCredit);
                    var param = repo.cmd.Parameters.AddWithValue("@tafiliGroupList",model.TafsiliGroupBinding);
                    param.SqlDbType = System.Data.SqlDbType.Structured;
                     var param1 = repo.cmd.Parameters.AddWithValue("@peopleGroupList",model.PeopleGroupBinding);
                    param1.SqlDbType = System.Data.SqlDbType.Structured;
                    repo.ExecuteNonQuery();
                    return new Models.ActionResultModelBinding
                    {
                        Message = repo.rMsg,
                        Status = repo.rCode == 1 ? Models.ActionResult.Success : Models.ActionResult.Failed
                    };
                }
            }
            catch (Exception c)
            {
                return new Models.ActionResultModelBinding
                {
                    Message = c.Message,
                    Status = Models.ActionResult.Failed
                };
            }
        }
        /// <summary>
        /// ویرایش حساب تفصیل
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding UpdateTafsil(Models.Tafsil model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updateTafsilAccount"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", model.Id);
                    repo.cmd.Parameters.AddWithValue("@code", model.Code);
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@automateDocumentTitle", model.TitleInAutomateDocument);
                    repo.cmd.Parameters.AddWithValue("@isActive", model.IsActive);
                    repo.cmd.Parameters.AddWithValue("@hasCostCenter", model.IsCostCenter);
                    repo.cmd.Parameters.AddWithValue("@fk_typ_entity_type", model.EntityTypeId);
                    repo.cmd.Parameters.AddWithValue("@tel", model.Tel);
                    repo.cmd.Parameters.AddWithValue("@mobile", model.Mobile);
                    repo.cmd.Parameters.AddWithValue("@address", model.Address);
                    repo.cmd.Parameters.AddWithValue("@nationalCode", model.NationalCode);
                    repo.cmd.Parameters.AddWithValue("@economyCode", model.EconomyCode);
                    repo.cmd.Parameters.AddWithValue("@accTypeId", model.AccountTypeId);
                    repo.cmd.Parameters.AddWithValue("@maxCredit", model.MaxCredit);
                    var param = repo.cmd.Parameters.AddWithValue("@tafiliGroupList",model.TafsiliGroupBinding);
                    param.SqlDbType = System.Data.SqlDbType.Structured;
                     var param1 = repo.cmd.Parameters.AddWithValue("@peopleGroupList",model.PeopleGroupBinding);
                    param1.SqlDbType = System.Data.SqlDbType.Structured;
                    repo.ExecuteNonQuery();
                    return new Models.ActionResultModelBinding
                    {
                        Message = repo.rMsg,
                        Status = repo.rCode == 1 ? Models.ActionResult.Success : Models.ActionResult.Failed
                    };
                }
            }
            catch (Exception c)
            {
                return new Models.ActionResultModelBinding
                {
                    Message = c.Message,
                    Status = Models.ActionResult.Failed
                };
            }
        }
        /// <summary>
        /// حذف حساب تفصیل
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding DeleteTafsil(Models.Tafsil model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updateTafsilAccount"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", model.Id);
                    repo.ExecuteNonQuery();
                    return new Models.ActionResultModelBinding
                    {
                        Message = repo.rMsg,
                        Status = repo.rCode == 1 ? Models.ActionResult.Success : Models.ActionResult.Failed
                    };
                }
            }
            catch (Exception c)
            {
                return new Models.ActionResultModelBinding
                {
                    Message = c.Message,
                    Status = Models.ActionResult.Failed
                };
            }
        }
    }
}
