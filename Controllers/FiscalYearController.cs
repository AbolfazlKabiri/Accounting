using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    class FiscalYearController:Repository.Repository
    {
            /// <summary>
        /// ثبت سال مالی جديد
        /// </summary>
        /// <param name="model">
        /// مدل پايه سال مالی
        /// </param>
        /// <returns></returns>
        public Models.ActionResultModelBinding InsertFiscalYear(Models.FiscalYear model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_insertFiscalYear"))
                {
                    
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@startDate", model.StartDate);
                    repo.cmd.Parameters.AddWithValue("@endDate", model.EndDate);
                    repo.cmd.Parameters.AddWithValue("@taxable", model.Taxable);
                    repo.cmd.Parameters.AddWithValue("@taxes", model.Taxes);
                    repo.cmd.Parameters.AddWithValue("@duties", model.Duties);
                    repo.cmd.Parameters.AddWithValue("@is_active", model.IsActive);
                 
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
        /// دريافت ليست سال های مالی
        /// </summary>
        /// <param name="_pageNo">
        /// شماره صفحه 
        /// </param>
        /// <param name="_seedNumber">
        /// تعداد ركوردهاي مدنظر براي مشاهده در خروجي
        /// </param>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.FiscalYear> GetFiscalYears(short _pageNo = 0,short _seedNumber = 10,string search = null)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getFiscalYearList",true))
                {
                    pageNo = _pageNo;
                    seedNumber = _seedNumber;
                    
                   
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                   
                    return new Models.SelectResultModelBinding<Models.FiscalYear>
                    {
                        Body = info.Select(i => new Models.FiscalYear()
                        {
                            Id = Convert.ToInt32(i.Field<object>("id")),
                            Title = Convert.ToString(i.Field<object>("title")),
                            StartDate = Convert.ToString(i.Field<object>("startDate")),
                            EndDate = Convert.ToString(i.Field<object>("endDate")),
                            Taxable = Convert.ToBoolean(i.Field<object>("taxable")),
                            Taxes = Convert.ToDecimal(i.Field<object>("taxes")),
                            Duties = Convert.ToDecimal(i.Field<object>("duties")),
                            IsActive = Convert.ToBoolean(i.Field<object>("is_active")),
                            Status = Convert.ToString(i.Field<object>("status")),
                            
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
        /// ويرايش سال مالی
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding UpdateFiscalYear(Models.FiscalYear model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updateFiscalYear"))
                {
                    repo.cmd.Parameters.AddWithValue("@id",model.Id);
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@startDate", model.StartDate);
                    repo.cmd.Parameters.AddWithValue("@endDate", model.EndDate);
                    repo.cmd.Parameters.AddWithValue("@taxable", model.Taxable);
                    repo.cmd.Parameters.AddWithValue("@taxes", model.Taxes);
                    repo.cmd.Parameters.AddWithValue("@duties", model.Duties);
                    repo.cmd.Parameters.AddWithValue("@is_active", model.IsActive);

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
