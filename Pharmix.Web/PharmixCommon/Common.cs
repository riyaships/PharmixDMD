using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PharmixCommon
{
    public class Common
    {
        public string dbConnectionString = "Server=DESKTOP-FVE0B6M\\SQLEXPRESS;Database=dmd;Integrated Security=False;User Id=sa;Password=123;MultipleActiveResultSets=true";
        //public string dbConnectionString = "Server=DESKTOP-49T3KPL;Database=Upwork;User ID=sa;Password=123";
        public DataSet GetDataSet(string sqlquery,string connectionString,string path)
        {
            DataSet _dsResult = new DataSet();
            try
            {
                //TraceService("connectionString " , connectionString);
                SqlConnection _connection = new SqlConnection(connectionString);
                SqlCommand _command = new SqlCommand(sqlquery, _connection);
                _command.CommandTimeout = 0;
                SqlDataAdapter _Adapter = new SqlDataAdapter(_command);
                _Adapter.Fill(_dsResult);
                
            }
            catch (Exception ex)
            {

                TraceService("Eror : " + ex.Message,path);
            }

            return _dsResult;

        }

        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>(); 
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        private void TraceService(string content,string path)
        {

            //set up a filestream
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

            //set up a streamwriter for adding text
            StreamWriter sw = new StreamWriter(fs);

            //find the end of the underlying filestream
            sw.BaseStream.Seek(0, SeekOrigin.End);

            //add the text
            sw.WriteLine(content);
            //add the text to the underlying filestream

            sw.Flush();
            //close the writer
            sw.Close();


        }

        #region Execute Query 

        public int ExecuteQuery(string _query, string connectionString)
        {
            SqlConnection _connection=null;
            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                SqlCommand _command = new SqlCommand(_query, _connection);
                int _count = _command.ExecuteNonQuery();
                _connection.Close();
                return _count;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _connection.Close();
            }

        }

        #endregion
    }
}
