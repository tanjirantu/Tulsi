using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulsi.BusinessObject;

namespace Tulsi.DataLayer
{
    public class MedicineDAL
    {
        private DataAccess db;
        private SqlCommand command;

        public MedicineDAL()
        {
            this.db = new DataAccess();
        }

        public Medicine Get(int id)
        {
            string sql = "SELECT * FROM Medicines WHERE Id=@id";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@id", id);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            Medicine m = new Medicine();
            if (reader.Read())
            {
                m.Id = Convert.ToInt32(reader["Id"]);
                m.BrandName = reader["BrandName"].ToString();
                //m.GenericName = reader["GenericName"].ToString();
                //m.FormulationType = reader["FormulationType"].ToString();
                //m.DosageAmount = reader["DosageAmount"].ToString();
                //m.Manufacturer = reader["CompanyName"].ToString();
                m.UnitPrice = Convert.ToDouble(reader["UnitPrice"]);
                m.Qty = Convert.ToInt32(reader["Quantity"]);
            }

            this.db.Close();
            return m;
        }

        public List<Medicine> GetAllDetails()
        {
            string sql = "SELECT [Medicines].[Id], [Medicines].[BrandName], [Generics].[GenericName], [Formulations].[FormulationType], [Dosages].[DosageAmount], [Manufacturers].[CompanyName], [Medicines].[UnitPrice], [Medicines].[SellingPrice], [Medicines].[Quantity], [Medicines].[ExpireDate] "
                        +"FROM [Medicines] "
                        +"INNER JOIN [Generics] ON "
                        +"[Medicines].[GenericId] = [Generics].[Id] "
                        +"INNER JOIN [Formulations] ON "
                        +"[Medicines].[FormulationId] = [Formulations].[Id] "
                        +"INNER JOIN [Dosages] ON "
                        +"[Medicines].[DosageId] = [Dosages].[Id] "
                        +"INNER JOIN [Manufacturers] ON"
                        +"[Medicines].[ManufacturerId] = [Manufacturers].[Id]";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<Medicine> mlist = new List<Medicine>();
            while (reader.Read())
            {
                Medicine m = new Medicine();

                m.Id = Convert.ToInt32(reader["Id"]);
                m.BrandName = reader["BrandName"].ToString();
                m.GenericName = reader["GenericName"].ToString();
                m.FormulationType = reader["FormulationType"].ToString();
                m.DosageAmount = reader["DosageAmount"].ToString();
                m.Manufacturer = reader["CompanyName"].ToString();
                m.UnitPrice = Convert.ToDouble(reader["UnitPrice"]);
                m.SellingPrice = Convert.ToDouble(reader["SellingPrice"]);
                m.Qty = Convert.ToInt32(reader["Quantity"]);
                m.ExpireDate = Convert.ToDateTime(reader["ExpireDate"]);

                mlist.Add(m);
            }

            this.db.Close();
            return mlist;
        }

        

        //public List<Medicine> SearchByBrand(Medicine medicine)
        //{
        //    string sql = "SELECT * FROM Medicines WHERE FormulationId=@formulationId AND DosageId=@dosageId";
        //    this.command = new SqlCommand(sql, this.db.Conn);
        //    this.command.Parameters.AddWithValue("@formulationId", medicine.FormulationId);
        //    this.command.Parameters.AddWithValue("@dosageId", medicine.DosageId);
        //    this.db.Conn.Open();
        //    SqlDataReader reader = this.command.ExecuteReader();

        //    List<Medicine> mlist = new List<Medicine>();
        //    while (reader.Read())
        //    {
        //        Medicine m = new Medicine();
        //        m.Id = Convert.ToInt32(reader["Id"]);
        //        m.BrandName = reader["BrandName"].ToString();
        //        m.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);

        //        mlist.Add(m);
        //    }

        //    this.db.Close();
        //    return mlist;
        //}

        public List<Medicine> Search(Medicine medicine)
        {
            string sql = "SELECT [Medicines].[Id], [Medicines].[BrandName], [Generics].[GenericName], [Formulations].[FormulationType], [Dosages].[DosageAmount], [Manufacturers].[CompanyName], [Medicines].[UnitPrice], [Medicines].[SellingPrice], [Medicines].[Quantity], [Medicines].[ExpireDate] "
                        + "FROM [Medicines] "
                        + "INNER JOIN [Generics] ON "
                        + "[Medicines].[GenericId] = [Generics].[Id] "
                        + "INNER JOIN [Formulations] ON "
                        + "[Medicines].[FormulationId] = [Formulations].[Id] "
                        + "INNER JOIN [Dosages] ON "
                        + "[Medicines].[DosageId] = [Dosages].[Id] "
                        + "INNER JOIN [Manufacturers] ON"
                        + "[Medicines].[ManufacturerId] = [Manufacturers].[Id]"
                        + "WHERE [Medicines].[BrandName] LIKE '%' + @brandName + '%' ";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@brandName", medicine.BrandName);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            List<Medicine> mlist = new List<Medicine>();
            while (reader.Read())
            {
                Medicine m = new Medicine();

                m.Id = Convert.ToInt32(reader["Id"]);
                m.BrandName = reader["BrandName"].ToString();
                m.GenericName = reader["GenericName"].ToString();
                m.FormulationType = reader["FormulationType"].ToString();
                m.DosageAmount = reader["DosageAmount"].ToString();
                m.Manufacturer = reader["CompanyName"].ToString();
                m.UnitPrice = Convert.ToDouble(reader["UnitPrice"]);
                m.SellingPrice = Convert.ToDouble(reader["SellingPrice"]);
                m.Qty = Convert.ToInt32(reader["Quantity"]);
                m.ExpireDate = Convert.ToDateTime(reader["ExpireDate"]);

                mlist.Add(m);
            }

            this.db.Close();
            return mlist;
        }

        public int Update(Medicine m)
        {
            Medicine medicine = Get(m.Id);
            int totalQty = medicine.Qty;
            int toDelete = totalQty - m.Qty;

            string sql = "UPDATE Medicines SET Quantity=@quantity WHERE Id=@id";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@id", m.Id);
            this.command.Parameters.AddWithValue("@quantity", toDelete);
            this.db.Conn.Open();
            int affectedRows = this.command.ExecuteNonQuery();
            this.db.Close();
            return affectedRows;
        }

        public Medicine GetOrdableMedicinesQty()
        {
            string sql = "SELECT COUNT([Medicines].[Quantity]) AS Quantity FROM [Medicines] WHERE Quantity < 50";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.db.Conn.Open();
            SqlDataReader reader = this.command.ExecuteReader();

            Medicine m = new Medicine();
            if (reader.Read())
            {
                m.Qty = Convert.ToInt32(reader["Quantity"]);
            }

            this.db.Close();
            return m;
        }

        internal Medicine Get()
        {
            throw new NotImplementedException();
        }

        public int Insert(Medicine m)
        {
            DateTime date = DateTime.Now;
            string sql = "INSERT INTO Medicines (BrandName, GenericId, FormulationId, DosageId, ManufacturerId, Quantity, UnitPrice, SellingPrice, ExpireDate) VALUES (@brandName, @genericId, @formulationId, @dosageId, @manufacturerId, @quantity, @unitPrice, @sellingPrice, @expiredate)";
            this.command = new SqlCommand(sql, this.db.Conn);
            this.command.Parameters.AddWithValue("@brandName", m.BrandName);
            this.command.Parameters.AddWithValue("@genericId", m.GenericId);
            this.command.Parameters.AddWithValue("@formulationId", m.FormulationId);
            this.command.Parameters.AddWithValue("@dosageId", m.DosageId);
            this.command.Parameters.AddWithValue("@manufacturerId", m.ManufacturerId);
            this.command.Parameters.AddWithValue("@quantity", m.Qty);
            this.command.Parameters.AddWithValue("@unitPrice", m.UnitPrice);
            this.command.Parameters.AddWithValue("@sellingPrice", m.SellingPrice);
            this.command.Parameters.AddWithValue("@expireDate", m.ExpireDate);
            this.db.Conn.Open();
            int affectedRows = this.command.ExecuteNonQuery();
            this.db.Close();
            return affectedRows;
        }
    }
}
