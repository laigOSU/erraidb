using System;
using System.Data.SqlClient;   // System.Data.dll
//using System.Data;           // For:  SqlDbType , ParameterDirection

namespace csharp_db_test
{
    class Program
    {
        /***********************************
         MAIN
        ************************************/

        /****** START OF MAIN *******/
        static void Main(string[] args)
        {
            try
            {
                var cb = new SqlConnectionStringBuilder();
                cb.DataSource = "erraisqlserver.database.windows.net";
                cb.UserID = "erraiadmin";
                cb.Password = "DBaccess3";
                cb.InitialCatalog = "EmployeeDB";

                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    connection.Open();

                    Submit_Tsql_NonQuery(connection, "2 - Create-Tables", Build_2_Tsql_CreateTables());

                    //Submit_Tsql_NonQuery(connection, "3 - Inserts", Build_3_Tsql_Inserts());

                    //Submit_Tsql_NonQuery(connection, "4 - Update-Join", Build_4_Tsql_UpdateJoin(),
                    //"@csharpParmDepartmentName", "Accounting");

                    //Submit_Tsql_NonQuery(connection, "5 - Delete-Join", Build_5_Tsql_DeleteJoin(),
                    //"@csharpParmDepartmentName", "Legal");

                    //Submit_6_Tsql_SelectEmployees(connection);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


            Console.WriteLine("=================================");
            Console.WriteLine("Testing Output Portion");
            Console.WriteLine("=================================");

            Console.WriteLine("View the report output here, then press any key to end the program...");
            Console.ReadKey();
        }

        /****** END OF MAIN *******/

        // For SQL Syntax CheatSheet: https://tableplus.io/blog/2018/08/ms-sql-server-data-types-cheatsheet.html


        /***********************************
         SQL CREATE TABLES FUNCTION 
        ************************************/
        static string Build_2_Tsql_CreateTables()
        {
            return @"

        -- This section below (Microsoft demo tables) is commented out, but kept so we can use as template 

        -- DROP TABLE IF EXISTS tabEmployee;
        -- DROP TABLE IF EXISTS tabDepartment;  -- Drop parent table last.

        -- CREATE TABLE tabDepartment
        -- (
        --     DepartmentCode  nchar(4)          not null    PRIMARY KEY,
        --     DepartmentName  nvarchar(128)     not null
        -- );

        -- CREATE TABLE tabEmployee
        -- (
        --     EmployeeGuid    uniqueIdentifier  not null  default NewId()    PRIMARY KEY,
        --     EmployeeName    nvarchar(128)     not null,
        --     EmployeeLevel   int               not null,
        --     DepartmentCode  nchar(4)              null
        --     REFERENCES tabDepartment (DepartmentCode) -- (REFERENCES would be disallowed on temporary tables.)
        -- );

        -- Clean the DB:
        DROP TABLE IF EXISTS [user];
        DROP TABLE IF EXISTS tabDepartment;
        DROP TABLE IF EXISTS userAccount;

        -- Here, we create our actual EmployeeDB tables:

        DROP TABLE IF EXISTS award;
        DROP TABLE IF EXISTS role;
        DROP TABLE IF EXISTS permission;
        DROP TABLE IF EXISTS userAcct;

        CREATE TABLE userAcct
        (
            id          int IDENTITY(1,1)  not null  PRIMARY KEY,    -- IDENTIY(1,1) is SQL for auto_increment
            first_name  varchar(128)       not null,
            last_name   varchar(128)       not null,
            password    varchar(128)       not null,
            email       varchar(128)       not null,
            create_on   timestamp          not null,
            signature   image
        
        );

        CREATE TABLE permission
        (
            id          int IDENTITY(1,1) not null  PRIMARY KEY,    -- IDENTIY(1,1) is SQL for auto_increment
            addUser     bit null,                                   -- Will be 0, 1, or NULL        
            editUser    bit null,                                   -- Will be 0, 1, or NULL
            deleteUser  bit null                                    -- Will be 0, 1, or NULL
        )

        CREATE TABLE role
        (
            id              int IDENTITY(1,1)   not null    PRIMARY KEY,                -- IDENTIY(1,1) is SQL for auto_increment
            user_id         int                 not null    REFERENCES userAcct (id),   -- A foreign key to userAcct.id
            permission_id   int                 not null    REFERENCES permission (id), -- A foreign key to permission.id
            type            bit                 null                                    -- Will be 0, 1, or NULL
        )


        CREATE TABLE award 
        (
            id          int IDENTITY(1,1)   not null    PRIMARY KEY,            -- IDENTIY(1,1) is SQL for auto_increment                
            role_id     int                 not null    REFERENCES role (id),   -- A foreign key to role.id
            type        int                 null,                               -- Can be of many types. Default is null.
            time        time                null,
            date        date                null
        )
    ";
        }


        /***********************************
         SQL INSERT FUNCTION
        ************************************/
        static string Build_3_Tsql_Inserts()
        {
            return @"
        -- The company has these departments.
        INSERT INTO tabDepartment (DepartmentCode, DepartmentName)
        VALUES
            ('acct', 'Accounting'),
            ('hres', 'Human Resources'),
            ('legl', 'Legal');

        -- The company has these employees, each in one department.
        INSERT INTO tabEmployee (EmployeeName, EmployeeLevel, DepartmentCode)
        VALUES
            ('Alison'  , 19, 'acct'),
            ('Barbara' , 17, 'hres'),
            ('Carol'   , 21, 'acct'),
            ('Deborah' , 24, 'legl'),
            ('Elle'    , 15, null);
    ";
        }

        /***********************************
          SQL UPDATE FUNCTION
        ************************************/
        static string Build_4_Tsql_UpdateJoin()
        {
            return @"
        DECLARE @DName1  nvarchar(128) = @csharpParmDepartmentName;  --'Accounting';

        -- Promote everyone in one department (see @parm...).
        UPDATE empl
        SET
            empl.EmployeeLevel += 1
        FROM
            tabEmployee   as empl
        INNER JOIN
            tabDepartment as dept ON dept.DepartmentCode = empl.DepartmentCode
        WHERE
            dept.DepartmentName = @DName1;
    ";
        }

        /***********************************
          SQL DELETE FUNCTION
        ************************************/
        static string Build_5_Tsql_DeleteJoin()
        {
            return @"
        DECLARE @DName2  nvarchar(128);
        SET @DName2 = @csharpParmDepartmentName;  --'Legal';

        -- Right size the Legal department.
        DELETE empl
        FROM
            tabEmployee   as empl
        INNER JOIN
            tabDepartment as dept ON dept.DepartmentCode = empl.DepartmentCode
        WHERE
            dept.DepartmentName = @DName2

        -- Disband the Legal department.
        DELETE tabDepartment
            WHERE DepartmentName = @DName2;
    ";
        }



        /***********************************
          SQL SELECT FUNCTION
        ************************************/
        static string Build_6_Tsql_SelectEmployees()
        {
            return @"
        -- Look at all the final Employees.
        SELECT
            empl.EmployeeGuid,
            empl.EmployeeName,
            empl.EmployeeLevel,
            empl.DepartmentCode,
            dept.DepartmentName
        FROM
            tabEmployee   as empl
        LEFT OUTER JOIN
            tabDepartment as dept ON dept.DepartmentCode = empl.DepartmentCode
        ORDER BY
            EmployeeName;
    ";
        }

        static void Submit_6_Tsql_SelectEmployees(SqlConnection connection)
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("Now, SelectEmployees (6)...");

            string tsql = Build_6_Tsql_SelectEmployees();

            using (var command = new SqlCommand(tsql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} , {1} , {2} , {3} , {4}",
                            reader.GetGuid(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            (reader.IsDBNull(3)) ? "NULL" : reader.GetString(3),
                            (reader.IsDBNull(4)) ? "NULL" : reader.GetString(4));
                    }
                }
            }
        }


        /***********************************
          DISPLAY OUTPUT FUNCTION
        ************************************/
        static void Submit_Tsql_NonQuery(
            SqlConnection connection,
            string tsqlPurpose,
            string tsqlSourceCode,
            string parameterName = null,
            string parameterValue = null
            )
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("T-SQL to {0}...", tsqlPurpose);

            using (var command = new SqlCommand(tsqlSourceCode, connection))
            {
                if (parameterName != null)
                {
                    command.Parameters.AddWithValue(  // Or, use SqlParameter class.
                        parameterName,
                        parameterValue);
                }
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " = rows affected.");
            }
        }
    } // EndOfClass
}

