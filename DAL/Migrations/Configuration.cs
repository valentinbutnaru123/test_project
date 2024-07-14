namespace DAL.Migrations
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Data.Entity.ModelConfiguration.Configuration;
	using System.Linq;
	using System.Runtime.Remoting.Contexts;
	using DAL.Common;
	using DAL.Entities;

	internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(DataAccessLayer.ApplicationDbContext context)
		{
			string password = "1234";
			byte[] salt = PasswordHasher.GenerateSalt();
			byte[] encryptedStrig = PasswordHasher.HashPassword(password, salt);



			context.UserTypes.AddOrUpdate(u => u.UserType,
				new Entities.UserTypeEntity
				{
					UserType = "admin"
				});
			context.SaveChanges();

			context.UserTypes.AddOrUpdate(u => u.UserType,
				new Entities.UserTypeEntity
				{
					UserType = "tehnical group"
				});
			context.SaveChanges();


			context.Users.AddOrUpdate(u => u.Name,
				new Entities.UserEntity
				{
					Name = "crme1",
					Email = "crme1@gmail.com",
					PasswordHash = encryptedStrig,
					Login = "crme1",
					Telephone = "55424525",
					UserTypeId = 1,
					Salt = salt

				});
			context.SaveChanges();

			context.Users.AddOrUpdate(u => u.Name,
				new Entities.UserEntity
				{
					Name = "crme2",
					Email = "crme2@gmail.com",
					PasswordHash = encryptedStrig,
					Login = "crme2",
					Telephone = "5542425",
					UserTypeId = 2,
					Salt = salt
				});
			context.SaveChanges();


			context.Cities.AddOrUpdate(u => u.CityName,
			new Entities.CityEntity
			{
				CityName = "Chisinau"
			});
			context.SaveChanges();


			context.Cities.AddOrUpdate(u => u.CityName,
			new Entities.CityEntity
			{
				CityName = "Orhei"
			});
			context.SaveChanges();


			context.Cities.AddOrUpdate(u => u.CityName,
		   new Entities.CityEntity
		   {
			   CityName = "Milano"
		   });
			context.SaveChanges();



			context.ConnectionTypes.AddOrUpdate(u => u.ConnectionType,
			   new Entities.ConnectionTypeEntity
			   {
				   ConnectionType = "Wi-Fi"
			   });
			context.SaveChanges();

			context.ConnectionTypes.AddOrUpdate(u => u.ConnectionType,
				new Entities.ConnectionTypeEntity
				{
					ConnectionType = "Remote"
				});
			context.SaveChanges();



			//Priority
			context.Priorities.AddOrUpdate(u => u.PriorityName,
				new PriorityEntity
				{ 
					PriorityName = "Normal"
				});
			context.SaveChanges();

			context.Priorities.AddOrUpdate(u => u.PriorityName,
				new PriorityEntity
				{
					PriorityName = "Urgent"
				});
			context.SaveChanges();




			//status
			context.Statuses.AddOrUpdate(u => u.Status,
				new StatusEntity
				{
					Status = "New"

				});
			context.SaveChanges();

			context.Statuses.AddOrUpdate(u => u.Status,
				new StatusEntity
				{
					Status = "Assigned"

				});
			context.SaveChanges();

			context.Statuses.AddOrUpdate(u => u.Status,
				new StatusEntity
				{
					Status = "In progress"

				});
			context.SaveChanges();

			context.Statuses.AddOrUpdate(u => u.Status,
				new StatusEntity
				{
					Status = "Pending"

				});
			context.SaveChanges();



			//IsueType

			// Hardware Issues
			context.IssuesType.AddOrUpdate(u => u.Name,
			new IssuesTypeEntity
			{
				Id = 1,
				IssueLevel = 1,
				Name = "Hardware Issues",
				InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
			});

			context.IssuesType.AddOrUpdate(u => u.Name,
		   new IssuesTypeEntity
		   {
			   Id = 2,
			   IssueLevel = 2,
			   ParentIssues = 1,
			   Name = "Device Malfunction",
			   InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		   });

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 3,
			IssueLevel = 3,
			ParentIssues = 2,
			Name = "POS Terminal not powering on",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 4,
			IssueLevel = 3,
			ParentIssues = 2,
			Name = "Printer issues (not printing, paper jams)",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});
			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 5,
			IssueLevel = 3,
			ParentIssues = 2,
			Name = "Barcode scanner failure",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});
			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 6,
			IssueLevel = 3,
			ParentIssues = 2,
			Name = "Card reader malfunction",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 7,
			IssueLevel = 3,
			ParentIssues = 2,
			Name = "Cash drawer not opening",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 8,
			IssueLevel = 2,
			ParentIssues = 1,
			Name = "Connectivity Problems",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 9,
			IssueLevel = 3,
			ParentIssues = 8,
			Name = "Network issues (Wi-Fi/Ethernet connectivity)",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 10,
			IssueLevel = 3,
			ParentIssues = 8,
			Name = "Peripheral devices not connecting (USB, Bluetooth)",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});
			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 11,
			IssueLevel = 3,
			ParentIssues = 8,
			Name = "Power supply problems",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 12,
			IssueLevel = 2,
			ParentIssues = 1,
			Name = "Peripheral Compatibility",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 13,
			IssueLevel = 3,
			ParentIssues = 12,
			Name = "Printer, scanner, or cash drawer incompatibility",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 14,
			IssueLevel = 3,
			ParentIssues = 12,
			Name = "Device driver issues",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			// Software Issues
			context.IssuesType.AddOrUpdate(u => u.Name,
			new IssuesTypeEntity
			{
				Id = 15,
				IssueLevel = 1,
				Name = "Software Issues",
				InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
			});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 16,
			IssueLevel = 2,
			ParentIssues = 15,
			Name = "Application Errors",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 17,
			IssueLevel = 3,
			ParentIssues = 16,
			Name = "POS software crashing or freezing",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 18,
			IssueLevel = 3,
			ParentIssues = 16,
			Name = "Error messages during transactions",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 19,
			IssueLevel = 3,
			ParentIssues = 16,
			Name = "Database connectivity issues",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 20,
			IssueLevel = 3,
			ParentIssues = 16,
			Name = "Update failures or incompatibility",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 21,
			IssueLevel = 2,
			ParentIssues = 15,
			Name = "User Interface Issues",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 22,
			IssueLevel = 3,
			ParentIssues = 21,
			Name = "Slow response times",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 23,
			IssueLevel = 3,
			ParentIssues = 21,
			Name = "Display errors",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 24,
			IssueLevel = 3,
			ParentIssues = 21,
			Name = "Navigation problems",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 25,
			IssueLevel = 2,
			ParentIssues = 15,
			Name = "Configuration Problems",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
	new IssuesTypeEntity
	{
		Id = 26,
		IssueLevel = 3,
		ParentIssues = 25,
		Name = "Incorrect system settings",
		InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
	});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 27,
			IssueLevel = 3,
			ParentIssues = 25,
			Name = "User permissions not properly set",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 28,
			IssueLevel = 3,
			ParentIssues = 25,
			Name = "Misconfigured tax rates or discounts",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			// Transaction Issues
			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 29,
			IssueLevel = 1,
			Name = "Transaction Issues",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 30,
			IssueLevel = 2,
			ParentIssues = 29,
			Name = "Payment Processing",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 31,
			IssueLevel = 3,
			ParentIssues = 30,
			Name = "Credit/debit card not accepted",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 32,
			IssueLevel = 3,
			ParentIssues = 30,
			Name = "Transaction timeouts",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 33,
			IssueLevel = 3,
			ParentIssues = 30,
			Name = "Mobile payment issues (Apple Pay, Google Wallet)",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 34,
			IssueLevel = 2,
			ParentIssues = 29,
			Name = "Receipt Problems",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
			new IssuesTypeEntity
			{
				Id = 35,
				IssueLevel = 3,
				ParentIssues = 34,
				Name = "Incorrect receipt information",
				InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
			});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 36,
			IssueLevel = 3,
			ParentIssues = 34,
			Name = "Receipt printing errors",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 37,
			IssueLevel = 3,
			ParentIssues = 34,
			Name = "Digital receipt issues (email/SMS)",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 38,
			IssueLevel = 2,
			ParentIssues = 29,
			Name = "Inventory Management",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 39,
			IssueLevel = 3,
			ParentIssues = 38,
			Name = "Stock levels not updating correctly",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 40,
			IssueLevel = 3,
			ParentIssues = 38,
			Name = "Inaccurate inventory data",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 41,
			IssueLevel = 3,
			ParentIssues = 38,
			Name = "Problems with barcode scanning",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});


			// Security Issues
			context.IssuesType.AddOrUpdate(u => u.Name,
			new IssuesTypeEntity
			{
				Id = 42,
				IssueLevel = 1,
				Name = "Security Issues",
				InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
			});

			context.IssuesType.AddOrUpdate(u => u.Name,
			new IssuesTypeEntity
			{
				Id = 43,
				IssueLevel = 2,
				ParentIssues = 42,
				Name = "Data Breach",
				InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
			});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 44,
			IssueLevel = 3,
			ParentIssues = 43,
			Name = "Unauthorized access to the POS system",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 45,
			IssueLevel = 3,
			ParentIssues = 43,
			Name = "Credit card data theft",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 46,
			IssueLevel = 2,
			ParentIssues = 42,
			Name = "Software Vulnerabilities",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 47,
			IssueLevel = 3,
			ParentIssues = 46,
			Name = "Exploits in POS software",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 48,
			IssueLevel = 3,
			ParentIssues = 46,
			Name = "Malware infections",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 49,
			IssueLevel = 2,
			ParentIssues = 42,
			Name = "User Authentication",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});

			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 50,
			IssueLevel = 3,
			ParentIssues = 49,
			Name = "Weak or compromised passwords",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});
			context.IssuesType.AddOrUpdate(u => u.Name,
		new IssuesTypeEntity
		{
			Id = 51,
			IssueLevel = 3,
			ParentIssues = 49,
			Name = "Issues with user login/logout processes",
			InsertDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
		});



		}


	}

}


