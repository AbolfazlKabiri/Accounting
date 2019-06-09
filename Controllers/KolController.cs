using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    class KolController:Repository.Repository
    {
          /// <summary>
        /// ثبت حساب کل جديد
        /// </summary>
        /// <param name="kolModel">
        /// مدل پايه حساب کل
        /// </param>
        /// <returns></returns>
        public Models.ActionResultModelBinding InsertKolAccount(Models.Kol kolModel)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_insertAccountingKol"))
                {
                    repo.cmd.Parameters.AddWithValue("@code", kolModel.Code);
                    repo.cmd.Parameters.AddWithValue("@title", kolModel.Title);
                    repo.cmd.Parameters.AddWithValue("@natureId", kolModel.NatureId);
                    repo.cmd.Parameters.AddWithValue("@isDefault", kolModel.IsDefault);
                    repo.cmd.Parameters.AddWithValue("@groupId", kolModel.GroupId);
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
        /// دريافت ليست حسابهای کل
        /// </summary>
        /// <param name="_pageNo">
        /// شماره صفحه 
        /// </param>
        /// <param name="_seedNumber">
        /// تعداد ركوردهاي مدنظر براي مشاهده در خروجي
        /// </param>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.Kol> GetKolAccounts(short _pageNo = 0,short _seedNumber = 10,string search = null)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getAccountKolList",true))
                {
                    pageNo = _pageNo;
                    seedNumber = _seedNumber;
                    
                    repo.cmd.Parameters.AddWithValue("@search", search);
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                    return new Models.SelectResultModelBinding<Models.Kol>
                    {
                        Body = info.Select(i => new Models.Kol()
                        {
                            Id = Convert.ToInt32(i.Field<object>("id")),
                            Code = Convert.ToString(i.Field<object>("code")),
                            Title = Convert.ToString(i.Field<object>("title")),
                            IsDefault = Convert.ToBoolean(i.Field<object>("is_default")),
                            NatureId = Convert.ToInt16(i.Field<object>("fk_nature_id")),
                            NatureTitle = Convert.ToString(i.Field<object>("natureTitle")),
                            GroupId = Convert.ToInt32(i.Field<object>("groupId")),
                            GroupTitle = Convert.ToString(i.Field<object>("groupTitle")),
                            GroupCode = Convert.ToString(i.Field<object>("groupCode")),
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
        /// ويرايش حساب کل
        /// </summary>
        /// <param name="kolModel"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding UpdateKolAccount(Models.Kol kolModel)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updateAccountingKol"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", kolModel.Id);
                    repo.cmd.Parameters.AddWithValue("@code", kolModel.Code);
                    repo.cmd.Parameters.AddWithValue("@title", kolModel.Title);
                    repo.cmd.Parameters.AddWithValue("@natureId", kolModel.NatureId);
                    repo.cmd.Parameters.AddWithValue("@isDefault", kolModel.IsDefault);
                    repo.cmd.Parameters.AddWithValue("@groupId", kolModel.GroupId);
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
        /// <param name="kolModel"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding RemoveKolAccount(Models.Kol kolModel,bool confirmDeleteSubdivision)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_deleteAccountingKol"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", kolModel.Id);
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
