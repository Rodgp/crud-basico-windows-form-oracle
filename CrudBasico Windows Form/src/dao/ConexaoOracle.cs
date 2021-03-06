﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudBasico_Windows_Form.src.dao
{
    public class ConexaoOracle
    {
        public static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=DB-PC)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=DESENV)));User ID=root;Password=12345";//
        //public static string connectionString = "Data Source=DESENV;Persist Security Info=True;User ID=root;Password=12345;";

        public static bool ConexaoDesenv()
        {
            OracleConnection con = new OracleConnection(connectionString);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public static DataTable RetornaDados(string select)
        {
            OracleConnection con = new OracleConnection(connectionString);

            
            DataTable ds = new DataTable();
            if (ConexaoDesenv())
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    OracleDataAdapter Adapter = new OracleDataAdapter(select, con);
                    Adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
            else
            {
                System.Console.WriteLine("Falha de conexão com o banco de dados!");
            }
            return ds;
        }

        public static void ExecutaComando(string comando)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand comandosql = new OracleCommand();

            if (ConexaoDesenv())
            {
                comandosql.CommandText = comando;
                comandosql.Connection = con;
                con.Open();
                comandosql.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                System.Console.WriteLine("Falha de conexão com o banco de dados!");
            }
        }

        public static void ComandoComParametro(string comando, byte[] anexo)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand comandosql = new OracleCommand();

            if (ConexaoDesenv())
            {
                comandosql.CommandText = comando;
                comandosql.Connection = con;
                comandosql.Parameters.Add(":anexo", anexo);
                con.Open();
                comandosql.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                System.Console.WriteLine("Falha de conexão com o banco de dados!");
            }
        }      

        public static bool ValidaExistencia(string select)
        {
            OracleConnection con = new OracleConnection(connectionString);
            DataTable ds = new DataTable();
            if (ConexaoDesenv())
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OracleDataAdapter Adapter = new OracleDataAdapter(select, con);
                    Adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
            else
            {
                System.Console.WriteLine("Falha de conexão com o banco de dados!");
            }

            return ds.Rows.Count > 0 ? true : false;
        }
    }
}
