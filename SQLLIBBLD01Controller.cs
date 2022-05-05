using System;
using System.IO;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Windows;

namespace Thaw_Mix_Dashboard
{
    class SQLLIBBLD01Controller : IDisposable
    {
        
        private string connectionString = Properties.Settings.Default.SpecimenTracker_ConnectionString + "Password=RjirRDH&!x51%gG4N";
                       
        private SQLLIBBLD01DataContext sqlLibBldConnectionDataContext;

        public bool CheckIn(string trkNumber, string emp)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAll_SpecimenTracker_InsertEventDetail", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ApplicatinID", Properties.Settings.Default.ApplicationID));
                cmd.Parameters.Add(new SqlParameter("@SpecimenIdRaw", trkNumber));
                cmd.Parameters.Add(new SqlParameter("@EmpId", emp));
                cmd.Parameters.Add(new SqlParameter("@Details", "Bin»FRO"));
                cmd.Parameters.Add(new SqlParameter("@EventTypeId", "2"));

                SqlDataReader rdr = cmd.ExecuteReader();
                conn.Close();
                return true;
            }
        }

        public bool CheckOut(int applicationID, string trkNumber, string emp)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAll_SpecimenTracker_InsertEventDetail", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ApplicatinID", applicationID));
                cmd.Parameters.Add(new SqlParameter("@SpecimenIdRaw", trkNumber));
                cmd.Parameters.Add(new SqlParameter("@EmpId", emp));
                cmd.Parameters.Add(new SqlParameter("@Details", "Bin»FRO"));
                cmd.Parameters.Add(new SqlParameter("@EventTypeId", "3"));

                SqlDataReader rdr = cmd.ExecuteReader();
                conn.Close();
                return true;
            }
        }

        public bool ScanAttempt(string trkNumber, string emp, string details)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAll_SpecimenTracker_InsertEventDetail", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ApplicationID", Properties.Settings.Default.ApplicationID));
                cmd.Parameters.Add(new SqlParameter("@SpecimenIdRaw", trkNumber));
                cmd.Parameters.Add(new SqlParameter("@EmpId", emp));
                cmd.Parameters.Add(new SqlParameter("@Details", details));
                cmd.Parameters.Add(new SqlParameter("@EventTypeId", "1"));

                SqlDataReader rdr = cmd.ExecuteReader();
                conn.Close();
                return true;
            }
        }

        public int GetMostRecentInOrOut(int applicationID, string trkNumber)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("spAll_SpecimenTracker_GetMostRecentInOrOut", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
            }

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAll_SpecimenTracker_GetMostRecentInOrOut", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ApplicationID", applicationID));
                cmd.Parameters.Add(new SqlParameter("@SpecimenId", trkNumber));

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Read();
                    var eventType = rdr.GetString(0)[0];
                    rdr.Close();
                    conn.Close();
                    return eventType;
                }
                else
                {
                    rdr.Close();
                    conn.Close();
                    return 0;
                }
            }
        }

        public List<Specimen> GetCheckedInSpecimens()
        {
            ESPController eSPController = new ESPController();
            List<Specimen> specimens = new List<Specimen>();
            int current = 0;

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("spAll_SpecimenTracker_GetCheckedInSpecimens", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
            }

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spAll_SpecimenTracker_GetCheckedInSpecimens", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ApplicationID", Properties.Settings.Default.ApplicationID));
                cmd.Parameters.Add(new SqlParameter("@Minutes", Properties.Settings.Default.CheckedInTimeMinutes));

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Automatically checks out specimens whose IN LAB status is greater than or equal to 5. Displays all other checked in specimens on dashboard
                        int lisStatusID = Int32.Parse(eSPController.InLabStatus(reader.GetString(0)));
                        if (lisStatusID < 5)
                        {
                            specimens.Add(new Specimen(reader.GetString(0), reader.GetDateTime(1).ToString("MMM d h:mm tt"), reader.GetInt32(2).ToString()));
                            if (reader.GetDateTime(1) < DateTime.Now.AddHours(-1 * Properties.Settings.Default.CheckedInTimeHours))
                            {
                                specimens.ElementAt(current).foreground = "Red";
                            }
                            current++;
                        }
                        else if (lisStatusID >= 5)
                        {
                            this.CheckOut(Properties.Settings.Default.ApplicationID, reader.GetString(0), "-1");
                        }
                    }                
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }

            return specimens;
        }

        public void Dispose()
        {
            sqlLibBldConnectionDataContext.Dispose();
        }
    }
}