using System;
using System.Data;
using System.Linq;

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
        /// <summary>
        /// دريافت ليست حسابهای تفصیل
        /// </summary>
        /// <param name="_pageNo">
        /// شماره صفحه 
        /// </param>
        /// <param name="_seedNumber">
        /// تعداد ركوردهاي مدنظر براي مشاهده در خروجي
        /// </param>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.Tafsil> GetTafsilAccounts(short _pageNo = 0,short _seedNumber = 10,string search = null)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getTafsilAccountList",true))
                {
                    pageNo = _pageNo;
                    seedNumber = _seedNumber;
                    
                    repo.cmd.Parameters.AddWithValue("@search", search);
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                    var tafsiliGroupList = repo.ds.Tables[1].AsEnumerable();
                    var peopleGroupList = repo.ds.Tables[2].AsEnumerable();
                    return new Models.SelectResultModelBinding<Models.Tafsil>
                    {
                        Body = info.Select(i => new Models.Tafsil()
                        {
                            Id = Convert.ToInt32(i.Field<object>("id")),
                            Code = Convert.ToString(i.Field<object>("code")),
                            Title = Convert.ToString(i.Field<object>("title")),
                            TafsilMap = tafsiliGroupList.Where(t=> Convert.ToInt32(t.Field<object>("TafsilId")) == Convert.ToInt32(i.Field<object>("id"))).Select(h=> h.Field<object>("full_path").ToString()).FirstOrDefault(),
                            TitleInAutomateDocument = Convert.ToString(i.Field<object>("automate_document_title")),
                            IsActive = Convert.ToBoolean(i.Field<object>("active")),
                            IsCostCenter = Convert.ToBoolean(i.Field<object>("cost_center")),
                            EntityTypeId = Convert.ToInt32(i.Field<object>("entityId")),
                            TafsiliGroupBindingString = string.Join(",",tafsiliGroupList.Where(m=> Convert.ToInt32(m.Field<object>("TafsilId")) == Convert.ToInt32(i.Field<object>("id"))).Select(c=> c.Field<object>("title")).ToList())
                        }).ToList(),
                        TotalCount = repo.totalCount
                    };

                }
            }
            catch (Exception c)
            {
                System.Windows.MessageBox.Show(c.Message);
                return null;
            }
        }
    }
}
