using System;
using System.Data;
using System.Linq;

namespace Accounting.Controllers
{
    class TafsiliGroupController:Repository.Repository
    {
        /// <summary>
        /// ثبت گروه تفصیلی جدید
        /// </summary>
        /// <param name="model">
        /// مدل پايه گروه تفصیلی
        /// </param>
        /// <returns></returns>
        public Models.ActionResultModelBinding InsertTafsiliGroupAccount(Models.TafsiliGroup model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_insertTafisiliGroup"))
                {
                    repo.cmd.Parameters.AddWithValue("@code", model.Code);
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@editable", model.Editable);
                   
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
        /// دريافت ليست گروه های تفصیلی
        /// </summary>
        /// <param name="_pageNo">
        /// شماره صفحه 
        /// </param>
        /// <param name="_seedNumber">
        /// تعداد ركوردهاي مدنظر براي مشاهده در خروجي
        /// </param>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.TafsiliGroup> GetTafsiliGroupAccounts(short _pageNo = 0,short _seedNumber = 10,string search = null)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getTafisiliGroup",true))
                {
                    pageNo = _pageNo;
                    seedNumber = _seedNumber;
                    
                    repo.cmd.Parameters.AddWithValue("@search", search);
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                  
                    return new Models.SelectResultModelBinding<Models.TafsiliGroup>
                    {
                        Body = info.Select(i => new Models.TafsiliGroup
                        {
                            Id = Convert.ToInt32(i.Field<object>("id")),
                            Code = Convert.ToString(i.Field<object>("code")),
                            Title = Convert.ToString(i.Field<object>("title")),
                            Editable = Convert.ToBoolean(i.Field<object>("editable"))
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
        /// <summary>
        /// ويرايش گروه تفصیلی
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding UpdateTafsiliGroupAccount(Models.TafsiliGroup model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updateTafisiliGroup"))
                {
                    repo.cmd.Parameters.AddWithValue("@id",model.Id);
                    repo.cmd.Parameters.AddWithValue("@code", model.Code);
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@editable", model.Editable);
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
        /// حذف يك حساب کل
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding RemoveTafsiliGroupAccount(Models.TafsiliGroup model,bool confirmDeleteSubdivision)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_deleteTafsiliGroup"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", model.Id);
                   // repo.cmd.Parameters.AddWithValue("@confirmDeleteSubdivision", confirmDeleteSubdivision);
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
