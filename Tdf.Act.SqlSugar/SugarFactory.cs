using SqlSugar;
using System.Collections.Generic;
using System.Configuration;

namespace Tdf.Act.SqlSugar
{
    public class SugarFactory
    {
        private static readonly string SqlServerConnString =
        ConfigurationManager.ConnectionStrings["DefaultDBConnection"].ToString();

        // 禁止实例化
        private SugarFactory() { }

        public static SqlSugarClient GetInstance()
        {
            string connection = SqlServerConnString;
            var db = new SqlSugarClient(connection);
            // 设置关联表 (引用地址赋值，每次赋值都只是存储一个内存地址)
            db.SetMappingTables(SugarConfigs.MpList);
            return db;
        }

        public class SugarConfigs
        {
            //key类名 value表名
            public static List<KeyValue> MpList = new List<KeyValue>(){
                new KeyValue(){ Key="User", Value="Act_User"}
            };
        }


    }
}
