using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


using employeeRecognition.Extensions;
using employeeRecognition.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace employeeRecognition.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private DataTable dt { get; set; }

        private DbConnection sqlConnection = new DbConnection();

        [HttpPost("[action]")]
        public IActionResult<UserAcct> Index()
        {
            List<UserAcct> list = new List<UserAcct>();

            String query = $"SELECT first_name FROM userAcct WHERE email=" + "'{User.email}'" + "AND password=" + "'{User.password}'";

            //String sql = @query;
            SqlCommand cmd = @query;

            //Try this:
            // http://csharp.net-informations.com/data-providers/csharp-sqlcommand-executescalar.htm

            cmd.CommandType = CommandType.Text;
            object obj = cmd.ExecuteScalar();

            //http://csharp.net-informations.com/data-providers/csharp-sqlcommand-executescalar.htm

            if (obj == null) // No such username or password exist
                return "NotValid";
            else
            {
                return "Valid";
            }


            dt = sqlConnection.Connection(sql);

            foreach (DataRow row in dt.Rows)
            {
                var user = new UserAcct();
                user.id = (int)row["id"];
                user.first_name = row["first_name"].ToString();
                user.last_name = row["last_name"].ToString();
                user.email = row["email"].ToString();
                list.Add(user);
            }

            return list;
        }

        protected void Validate(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Text = "clicked!";
        }
        //https://code.msdn.microsoft.com/windowsapps/Login-Form-using-Windows-a2bd41b4

        // Create New
        [HttpPost("[action]")]
        public IActionResult Create([FromBody]UserAcct User)
        {
            if (ModelState.IsValid)
            {
                String query = $"INSERT INTO userAcct(first_name, last_name, password, email, role, signature) VALUES" +
                    $"('{User.first_name}', '{User.last_name}', '{User.password}', '{User.email}', {User.role}, '{User.signature}')";

                String sql = @query;

                Console.WriteLine("QUERY: " + sql);

                dt = sqlConnection.Connection(sql);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // Delete
        [HttpDelete("[action]")]
        public IActionResult Delete(int id)
        {
            String query = $"DELETE FROM userAcct WHERE userAcct.id = {id}";

            String sql = @query;

            Console.WriteLine("QUERY: " + sql);

            dt = sqlConnection.Connection(sql);

            return Ok();
        }

        // Edit
        [HttpPut("[action]")]
        public IActionResult Edit(int id, [FromBody]UserAcct User)
        {
            if (ModelState.IsValid)
            {
                String query = $"Update userAcct set first_name='{User.first_name}', last_name='{User.last_name}', password='{User.password}', email='{User.email}', role={User.role}, signature='{User.signature}'  WHERE userAcct.id={id}";

                String sql = @query;

                Console.WriteLine("QUERY: " + sql);

                dt = sqlConnection.Connection(sql);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}