﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FamilyExpenseTracker.MVC1.Models.FamilyExpense
{
    public class FamilyExpenseRepository
    {
        public List<FamilyExpense> GetFamilyExpenses()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using(SqlCommand command=new SqlCommand("DisplayNames", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                            using(SqlDataReader dr = command.ExecuteReader())
                            {
                                List<FamilyExpense> familyExpenses = new List<FamilyExpense>();
                                while (dr.Read())
                                {
                                    FamilyExpense familyExpense = new FamilyExpense()
                                    {
                                        ExpenseId = Convert.ToInt32(dr["ExpenseId"]),
                                        FamilyMemberId = Convert.ToInt32(dr["FamilyMemberId"]),
                                        Name = (dr["Name"].ToString()),
                                        Purpose = dr["Purpose"].ToString(),
                                        Amount = Convert.ToInt32(dr["Amount"]),
                                        Date = Convert.ToDateTime(dr["DateTime"])
                                    };
                                    familyExpenses.Add(familyExpense);
                                }
                                dr.Close();
                                return familyExpenses;
                            }
                        }
                    }
                    catch(SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            return null;
        }

        public List<string> GetNames()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using(SqlCommand command=new SqlCommand("GetAllFamilyMemberNames", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                            List<string> names = new List<string>();
                            names.Add("--select--");
                            using (SqlDataReader dr = command.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    names.Add(dr["Name"].ToString());
                                }
                                dr.Close();
                                return names;
                            }
                        }
                    }
                    catch(SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            return null;
        }

        public int AddFamilyExpense(FamilyExpense familyExpense)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using(SqlCommand command=new SqlCommand("AddFamilyExpense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MemberName", familyExpense.Name);
                    command.Parameters.AddWithValue("@purpose", familyExpense.Purpose);
                    command.Parameters.AddWithValue("@expense", familyExpense.Amount);
                    command.Parameters.AddWithValue("@date", familyExpense.Date);
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                            int status = command.ExecuteNonQuery();
                            return status;
                        }
                    }
                    catch(SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            return 0;
        }

        public FamilyExpense GetFamilyExpense(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using(SqlCommand command=new SqlCommand("DisplayNames", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                            using(SqlDataReader dr = command.ExecuteReader())
                            {
                                List<FamilyExpense> familyExpenses = new List<FamilyExpense>();
                                while (dr.Read())
                                {
                                    FamilyExpense familyExpense = new FamilyExpense()
                                    {
                                        ExpenseId = Convert.ToInt32(dr["ExpenseId"]),
                                        FamilyMemberId = Convert.ToInt32(dr["FamilyMemberId"]),
                                        Name = dr["Name"].ToString(),
                                        Purpose = dr["Purpose"].ToString(),
                                        Amount = Convert.ToInt32(dr["Amount"]),
                                        Date = Convert.ToDateTime(dr["DateTime"])
                                    };
                                    familyExpenses.Add(familyExpense);
                                }
                                dr.Close();
                                FamilyExpense familyExpenseResult = familyExpenses.Find(familyExpense => familyExpense.ExpenseId == id);
                                return familyExpenseResult;
                            }
                        }
                    }
                    catch(SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            return null;
        }

        public int EditFamilyExpense(FamilyExpense familyExpense)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using(SqlCommand command=new SqlCommand("EditFamilyExpense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@expenseId", familyExpense.ExpenseId);
                    command.Parameters.AddWithValue("@familyMemberId", familyExpense.ExpenseId);
                    //command.Parameters.AddWithValue("@name", familyExpense.Name);
                    command.Parameters.AddWithValue("@purpose", familyExpense.Purpose);
                    command.Parameters.AddWithValue("@amount", familyExpense.Amount);
                    command.Parameters.AddWithValue("@date", familyExpense.Date);
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                            int status = command.ExecuteNonQuery();
                            return status;
                        }
                    }
                    catch(SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            return 0;
        }

        public int DeleteFamilyExpense(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteFamilyExpense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@expenseId", id);
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                            int status = command.ExecuteNonQuery();
                            return status;
                        }
                    }
                    catch(SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            return 0;
        }
    }
}