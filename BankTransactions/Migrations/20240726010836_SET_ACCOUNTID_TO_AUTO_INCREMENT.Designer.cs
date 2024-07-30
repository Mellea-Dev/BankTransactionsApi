﻿// <auto-generated />
using BankTransactions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankTransactions.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20240726010836_SET_ACCOUNTID_TO_AUTO_INCREMENT")]
    partial class SET_ACCOUNTID_TO_AUTO_INCREMENT
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BankTransactions.Models.AccountModel", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountNumber");

                    b.Property<string>("AccountType");

                    b.Property<decimal>("Balance");

                    b.Property<int>("CustomerID");

                    b.HasKey("AccountID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BankTransactions.Models.CustomerModel", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("Fullname");

                    b.Property<string>("Phone");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BankTransactions.Models.TransactionModel", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber");

                    b.Property<decimal>("Amount");

                    b.Property<string>("Description");

                    b.Property<string>("FromAccountNumner");

                    b.Property<string>("ToAccountNumber");

                    b.Property<string>("Type");

                    b.HasKey("TransactionID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankTransactions.Models.AccountModel", b =>
                {
                    b.HasOne("BankTransactions.Models.CustomerModel", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
