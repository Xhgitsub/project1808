using System;
using System.Collections.Generic;
using Model;
using DAL;
using Newtonsoft.Json;
namespace BLL
{
    public class UserBll
    {

        public List<Citys> Sheng() 
        {
            string sql = $"select * from Citys where pid=-1";
            var ds = DAL.DBHelper.GetSet(sql);
            var str = JsonConvert.SerializeObject(ds.Tables[0]);
            var list = JsonConvert.DeserializeObject<List<Citys>>(str);
            return list;
        }

        public List<Citys> shi(string pid)
        {
            string sql = $"select * from Citys where pid={pid}";
            var ds = DAL.DBHelper.GetSet(sql);
            var str = JsonConvert.SerializeObject(ds.Tables[0]);
            var list = JsonConvert.DeserializeObject<List<Citys>>(str);
            return list;
        }

        public List<Citys> qu(string pid)
        {
            string sql = $"select * from Citys where pid={pid}";
            var ds = DAL.DBHelper.GetSet(sql);
            var str = JsonConvert.SerializeObject(ds.Tables[0]);
            var list = JsonConvert.DeserializeObject<List<Citys>>(str);
            return list;
        }

        public List<Users> GetUsers() 
        {
            string sql = $"select * from Users";
            var ds = DBHelper.GetSet(sql);
            var str = JsonConvert.SerializeObject(ds.Tables[0]);
            var list = JsonConvert.DeserializeObject<List<Users>>(str);
            return list;
        }
        public int AddData(Users u) 
        {
            string sql = $"insert into Users values('{u.Name}','{u.Sex}','{u.Age}','{u.Address}','{u.Hobby.TrimEnd(',')}','{u.Img}','{u.Remark}')";
            return DBHelper.ExcureNonQuery(sql);
        }

        public int Del(string ids) 
        {
            string sql = $"delete from Users where id in({ids})";
            return DBHelper.ExcureNonQuery(sql);
        }

        public Users GetById(string id) 
        {
            string sql = $"select * from Users where id={id}";
            var ds = DBHelper.GetSet(sql);
            var str = JsonConvert.SerializeObject(ds.Tables[0]);
            var list = JsonConvert.DeserializeObject<List<Users>>(str);
            return list[0];
        }

        public int Update(Users u) 
        {
            string sql = $"update Users set Name='{u.Name}',Sex='{u.Sex}',Age='{u.Age}',Address='{u.Address}',Hobby='{u.Hobby}',Img='{u.Img}',Remark='{u.Remark}' where Id={u.Id}";
            return DBHelper.ExcureNonQuery(sql);
        }
    }
}
