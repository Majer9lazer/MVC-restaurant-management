using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RestaurantManagement.DAL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class RestaurantManagement_DB : DbContext
    {
        public RestaurantManagement_DB()
            : base("name=RestaurantManagement_DB")
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<EatType> EatTypes { get; set; }
        public virtual DbSet<Emploee> Emploees { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emploee>()
                .Property(e => e.Phonenumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

        }

        public async Task<string> SendMessage(string email, string message)
        {
            MailAddress from = new MailAddress("csharp.sdp.162@gmail.com", "������� �� �������� ������ �����");

            try
            {
                MailAddress to = new MailAddress(email);
                MailMessage m = new MailMessage(from, to)
                {
                    Subject = "�������� ��� �������� ���������.",
                    Body = message
                };
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("csharp.sdp.162@gmail.com", "sdp123456789"),
                    EnableSsl = true
                };
                await smtp.SendMailAsync(m);
                return $"��������� �� ����� ({email}) ���� ����������";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string SetStandartData()
        {
            if (!Jobs.Any())
            {
                try
                {
                    Jobs.AddRange(new List<Job>()
                    {
                        new Job() {Jobtype = "��������"},
                        new Job() {Jobtype = "�����"},
                        new Job(){Jobtype = "��������"},
                        new Job(){Jobtype = "��������"}
                    });

                    if (!EatTypes.Any())
                    {
                        EatTypes.AddRange(new List<EatType>()
                        {
                            new EatType() {EatTypeName = "�������"},
                            new EatType() {EatTypeName = "����"},
                            new EatType() {EatTypeName = "����"},
                        });
                    }
                    SaveChanges();
                    return 0.ToString();
                }
                catch (Exception e)
                {

                    return e.ToString();
                }
            }
            return "1";
        }
    }
}
