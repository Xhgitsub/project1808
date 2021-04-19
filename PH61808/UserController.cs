using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using System.IO;
using DAL;
using System.Data;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using BLL;
namespace PH61808
{
    public class UserController : Controller
    {
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _host;

        public UserController(Microsoft.AspNetCore.Hosting.IHostingEnvironment host) 
        {
            _host = host;
        }
        UserBll bll = new UserBll();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add() 
        {
            return View();
        }

        public IActionResult Sheng() 
        {
            var list = bll.Sheng();
            return Ok(new { data=list});
        }
        public IActionResult Shi(string pid)
        {
            var list = bll.shi(pid);
            return Ok(new { data = list });
        }
        public IActionResult Qu(string pid)
        {
            var list = bll.qu(pid);
            return Ok(new { data = list });
        }

        public IActionResult AddData() 
        {
            var file = Request.Form.Files[0];
            var Id = Request.Form["Id"];
            var Name = Request.Form["Name"];
            var Sex = Request.Form["Sex"];
            var Age = Request.Form["Age"];
            var Address = Request.Form["Address"];
            var Hobby = Request.Form["Hobby"];
            var www = _host.ContentRootPath;
            var Remark = Request.Form["Remark"];
            var path = $"{www}/wwwroot/Record/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileName = file.FileName;
            using (Stream str = System.IO.File.Create($"{path}{fileName}"))
            {
                file.CopyTo(str);
            }
            Users u = new Users();
            u.Name = Name;
            u.Id =Convert.ToInt32(Id);
            u.Img = $"/Record/{fileName}";
            u.Sex = Sex;
            u.Age =Convert.ToInt32(Age);
            u.Address = Address;
            u.Remark = Remark;
            u.Hobby = Hobby;
            if (Id==0)
            {
                int n = bll.AddData(u);
                return Ok(new { state = n > 0 ? true : false, msg = n > 0 ? "添加成功" : "添加失败" });
            }
            else
            {
                int n = bll.Update(u);
                return Ok(new { state=n>0?true:false,msg=n>0?"修改成功":"修改失败"});
            }
            
        }

        public IActionResult GetData(int page,int limit,string name="") 
        {
            if (name==null)
            {
                name = "";
            }
            var list = bll.GetUsers();
            list = list.Where(s => s.Name.Contains(name)).ToList();
            var _list = list.Skip((page-1)*limit).Take(limit);
            return Ok(new {code=0,msg="",data=_list,count=list.Count });
        }

        public IActionResult Dels(string ids) 
        {
            ids = ids.TrimEnd(',');
            int n = bll.Del(ids);
            return Ok(new {state=n>0?true:false,msg=n>0?"删除成功":"删除失败" });
        }

        public IActionResult Excels() 
        {
            string sql=$"select * from Users";
            var ds = DBHelper.GetSet(sql);
            var ms = Excel(ds.Tables[0]);
            var Ctype = "Application/octet-stream";
            var fileName = $"{DateTime.Now.ToString("yyyyMMdd")}.xls";
            return File(ms.ToArray(),Ctype,fileName);
        }
        MemoryStream Excel(DataTable dt) 
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("sheet");
            var rows = dt.Rows.Count;
            var columns = dt.Columns.Count;
            var headrow = sheet.CreateRow(0);
            for (int i = 0; i < columns; i++)
            {
                var cel = headrow.CreateCell(i);
                cel.SetCellValue(dt.Columns[i].ColumnName);
            }

            for (int s = 0; s < rows; s++)
            {
               var row=sheet.CreateRow(s+1);
                for (int n = 0; n < columns; n++)
                {
                   var cel=row.CreateCell(n);
                    cel.SetCellValue(Convert.ToString(dt.Rows[s][n]));
                }
            }
            MemoryStream str = new MemoryStream();
            workbook.Write(str);
            return str;
        }

        public IActionResult GetById(string id) 
        {
            var list = bll.GetById(id);
            return Ok(new {data=list});
        }
    }
}
