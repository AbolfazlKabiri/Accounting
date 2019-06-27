using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    class MoinController:Repository.Repository
    {
          /// <summary>
        /// ثبت حساب معین جديد
        /// </summary>
        /// <param name="model">
        /// مدل پايه حساب معین
        /// </param>
        /// <returns></returns>
        public Models.ActionResultModelBinding InsertMoinAccount(Models.Moin model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_insertAccountingMoein"))
                {
                    repo.cmd.Parameters.AddWithValue("@code", model.Code);
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@natureId", model.NatureId);
                    repo.cmd.Parameters.AddWithValue("@isDefault", model.IsDefault);
                    repo.cmd.Parameters.AddWithValue("@isPermanent", model.IsPermanent);
                    repo.cmd.Parameters.AddWithValue("@kolId", model.KolId);
                    var param = repo.cmd.Parameters.AddWithValue("@tafiliGroupList",model.TafsiliGroupBinding);
                    param.SqlDbType = System.Data.SqlDbType.Structured;
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
        /// دريافت ليست حسابهای معین
        /// </summary>
        /// <param name="_pageNo">
        /// شماره صفحه 
        /// </param>
        /// <param name="_seedNumber">
        /// تعداد ركوردهاي مدنظر براي مشاهده در خروجي
        /// </param>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.Moin> GetMoinAccounts(short _pageNo = 0,short _seedNumber = 10,string search = null)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getAccountingMoinList",true))
                {
                    pageNo = _pageNo;
                    seedNumber = _seedNumber;
                    
                    repo.cmd.Parameters.AddWithValue("@search", search);
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                    var tafsiliGroupList = repo.ds.Tables[1].AsEnumerable();
                    return new Models.SelectResultModelBinding<Models.Moin>
                    {
                        Body = info.Select(i => new Models.Moin()
                        {
                            Id = Convert.ToInt32(i.Field<object>("moinId")),
                            Code = Convert.ToString(i.Field<object>("moinCode")),
                            Title = Convert.ToString(i.Field<object>("moinTitle")),
                            IsDefault = Convert.ToBoolean(i.Field<object>("moinIsDefault")),
                            NatureId = Convert.ToInt16(i.Field<object>("moinNature")),
                            NatureTitle = Convert.ToString(i.Field<object>("moinNatureTitle")),
                            KolId = Convert.ToInt32(i.Field<object>("kolId")),
                            KolTitle = Convert.ToString(i.Field<object>("kolTitle")),
                            KolCode = Convert.ToString(i.Field<object>("kolCode")),
                            MoinMap = Convert.ToString(i.Field<object>("moinMap")),
                            TafsiliGroupBinding = SetDataTable(tafsiliGroupList.Where(m=> Convert.ToInt32(m.Field<object>("moinId")) == Convert.ToInt32(i.Field<object>("moinId"))).Select(c=>Convert.ToInt64(c.Field<object>("id"))).ToList()),
                            TafsiliGroupBindingString = string.Join(",",tafsiliGroupList.Where(m=> Convert.ToInt32(m.Field<object>("moinId")) == Convert.ToInt32(i.Field<object>("moinId"))).Select(c=> c.Field<object>("title")).ToList())
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
        /// ويرايش حساب معین
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding UpdateMoinAccount(Models.Moin model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updateAccountingMoein"))
                {
                    repo.cmd.Parameters.AddWithValue("@id",model.Id);
                    repo.cmd.Parameters.AddWithValue("@code", model.Code);
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@natureId", model.NatureId);
                    repo.cmd.Parameters.AddWithValue("@isDefault", model.IsDefault);
                    repo.cmd.Parameters.AddWithValue("@isPermanent", model.IsPermanent);
                    repo.cmd.Parameters.AddWithValue("@kolId", model.KolId);
                    var param = repo.cmd.Parameters.AddWithValue("@tafiliGroupList",model.TafsiliGroupBinding);
                    param.SqlDbType = System.Data.SqlDbType.Structured;
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
        /// حذف يك حساب معین
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding RemoveMoinAccount(Models.Moin model,bool confirmDeleteSubdivision)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_deleteAccountingMoin"))
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

        public DataTable SetDataTable(List<long> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            DataRow row;
            foreach (var item in list)
            {
                row = dt.NewRow();
                row["id"] = item;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
