using System;
using System.Collections.Generic;
using ContosoMaintenance.WebAPI.Models;

namespace ContosoMaintenance.WebAPI.DummyData
{
    public class DummyDataContainer
    {
        public List<Job> Jobs = new List<Job>();
        public List<Customer> Customers = new List<Customer>();
        public List<Employee> Employees = new List<Employee>();
        public List<Location> Addresses = new List<Location>();
        public List<Part> Parts = new List<Part>();

        public DummyDataContainer()
        {
            PopulateAddresses();
            PopulateCustomers();
            PopulateEmployees();
            PopulateParts();
            PopulateJobs();
        }

        private void PopulateParts()
        {
            var part1 = new Part
            {
                Name = "Crankshaft F337",
                Manufacturer = "Bonus Materials",
                ModelNumber = "CE3874902726",
                SerialNumber = "456732037463823902826",
                PartNumber = "6262939036",
                PriceInUSD = 599.12M,
                ImageSource = "https://contosomaintenance.blob.core.windows.net/images-large/67a04a57-b51e-45d8-b75c-c343a273b6f7.png"
            };
            Parts.Add(part1);
            var part2 = new Part
            {
                Name = "Airplane Engine RFE-747",
                Manufacturer = "Lolz Roice",
                ModelNumber = "CE3874902728",
                SerialNumber = "456732037463823902828",
                PartNumber = "6262939038",
                PriceInUSD = 35000000M,
                ImageSource = "https://contosomaintenance.blob.core.windows.net/images-large/67a24a57-b51r-45d8-b75c-c343a273b6f8.png"
            };
            Parts.Add(part2);
        }

        void PopulateAddresses()
        {
            var address1 = new Location
            {
                FirstLineAddress = "82 Fulford Road",
                City = "Pensarn",
                ZipCode = "SA31 5UQ"
            };
            address1.Point = new Point(51.786209, -4.383567);
            Addresses.Add(address1);

            var address2 = new Location
            {
                FirstLineAddress = "58 Caxton Place",
                City = "Cadbury Heath",
                ZipCode = "BS30 3SD"
            };
            address2.Point = new Point(51.374297, -2.525476);
            Addresses.Add(address2);

            var address3 = new Location
            {
                FirstLineAddress = "22 Gloddaeth Street",
                City = "Bindal",
                ZipCode = "IV20 4AH"
            };
            address3.Point = new Point(57.722237, -3.900663);
            Addresses.Add(address3);

            var address4 = new Location
            {
                FirstLineAddress = "12 Warren Street",
                City = "West Hanningfield",
                ZipCode = "CM2 0ER"
            };
            address4.Point = new Point(51.748377, 0.473482);
            Addresses.Add(address4);

            var address5 = new Location
            {
                FirstLineAddress = "40 Henley Road",
                City = "Bourne End",
                ZipCode = "HP1 1NU"
            };
            address5.Point = new Point(51.722259, -0.610968);
            Addresses.Add(address5);

            var address6 = new Location
            {
                FirstLineAddress = "5 Princes Street",
                City = "Rodhuish",
                ZipCode = "TA24 4PF"
            };
            address6.Point = new Point(51.076015, -3.501786);
            Addresses.Add(address6);

            var address7 = new Location
            {
                FirstLineAddress = "78 Scrimshire Lane",
                City = "Askham",
                ZipCode = "CA10 5HN"
            };
            address7.Point = new Point(54.402629, -2.136548);
            Addresses.Add(address7);

            var address8 = new Location
            {
                FirstLineAddress = "19 Hampton Court Rd",
                City = "Southsea",
                ZipCode = "LL11 7XE"
            };
            address8.Point = new Point(52.936876, -3.361345);
            Addresses.Add(address8);

            var address9 = new Location
            {
                FirstLineAddress = "36 Wressle Road",
                City = "Plas Gogerddan",
                ZipCode = "SY23 0RD"
            };
            address9.Point = new Point(51.762533, -4.308665);
            Addresses.Add(address9);

            var address10 = new Location
            {
                FirstLineAddress = "45 Layburn Court",
                City = "Town Street",
                ZipCode = "IP27 5JE"
            };
            address10.Point = new Point(52.332529, 0.490763);
            Addresses.Add(address10);
        }

        Location GetRandomAddress()
        {
            Random rnd = new Random();
            int r = rnd.Next(Addresses.Count);
            return Addresses[r];
        }

        DateTime GetRandomDate(bool fromPast = true)
        {
            Random rnd = new Random();
            int days = rnd.Next(5, 50);
            if (fromPast)
            {
                return DateTime.Now.Subtract(new TimeSpan(days, 0, 0, 0));
            }
            else
            {
                return DateTime.Now.Add(new TimeSpan(days, 0, 0, 0));
            }
        }

        void PopulateCustomers()
        {
            var customer1 = new Customer
            {
                CompanyName = "Answers Aviation",
                ContactName = "Nathan Sutton",
                ContactNumber = "070 3814 7607",
                Address = GetRandomAddress(),
                Email = "nat@answersaviation.com",
                Website = "http://www.answersaviation.com"
            };
            Customers.Add(customer1);

            var customer2 = new Customer
            {
                CompanyName = "British Airlines",
                ContactName = "Michael Messina",
                ContactNumber = "070 2210 5005",
                Address = GetRandomAddress(),
                Email = "mimess@britishairlins.co.uk",
                Website = "http://www.britishairlines.co.uk"
            };
            Customers.Add(customer2);

            var customer3 = new Customer
            {
                CompanyName = "Bristol Aerodrome",
                ContactName = "Joseph Mitchell",
                ContactNumber = "077 3397 8555",
                Address = GetRandomAddress(),
                Email = "joseph@britstolaerodrome.co.uk",
                Website = "http://britstolaerodrome.co.uk"
            };
            Customers.Add(customer3);

            var customer4 = new Customer
            {
                CompanyName = "London Gatwin",
                ContactName = "Keith Kelly",
                ContactNumber = "079 8898 2729",
                Address = GetRandomAddress(),
                Email = "kieth.kelly@londongatwin.co.uk",
                Website = "http://www.londongatwin.co.uk"
            };
            Customers.Add(customer4);

            var customer5 = new Customer
            {
                CompanyName = "London Kiethrow",
                ContactName = "Craig Hazzard",
                ContactNumber = "079 3121 0342",
                Address = GetRandomAddress(),
                Email = "craighazzard@lkw.co.uk",
                Website = "http://www.lkw.co.uk"
            };
            Customers.Add(customer5);

            var customer6 = new Customer
            {
                CompanyName = "Manchester Heliport",
                ContactName = "Anita Tandy",
                ContactNumber = "070 2806 7851",
                Address = GetRandomAddress(),
                Email = "anita@manchesterheliport.co.uk",
                Website = "http://www.manchesterheliport.co.uk"
            };
            Customers.Add(customer6);

            var customer7 = new Customer
            {
                CompanyName = "Blackbush Airport",
                ContactName = "Tammy Lindberg",
                ContactNumber = "078 2743 9040",
                Address = GetRandomAddress(),
                Email = "tammy@blackbushairport.co.uk",
                Website = "http://www.blackbushairport.co.uk"
            };
            Customers.Add(customer7);

            var customer8 = new Customer
            {
                CompanyName = "Farnborough Airport",
                ContactName = "Gloria Brown",
                ContactNumber = "077 1968 2071",
                Address = GetRandomAddress(),
                Email = "gloria@farnboroughairport.co.uk",
                Website = "http://www.farnboroughairport.co.uk"
            };
            Customers.Add(customer8);

            var customer9 = new Customer
            {
                CompanyName = "Bryan Air",
                ContactName = "Anthony Robinson",
                ContactNumber = "079 8701 0274",
                Address = GetRandomAddress(),
                Email = "antony@bryanair.co.uk",
                Website = "http://www.bryanair.co.uk"
            };
            Customers.Add(customer9);

            var customer10 = new Customer
            {
                CompanyName = "Glasgow Airport",
                ContactName = "Anthony Hess",
                ContactNumber = "070 7889 9721",
                Address = GetRandomAddress(),
                Email = "antony@glasgowairport.co.uk",
                Website = "http://www.glasgowairport.co.uk"
            };
            Customers.Add(customer10);

            var customer11 = new Customer
            {
                CompanyName = "Sturgate Airfield",
                ContactName = "Sandra Blythe",
                ContactNumber = "079 4629 5754",
                Address = GetRandomAddress(),
                Email = "sandra@sturgateairfield.co.uk",
                Website = "http://www.sturgateairfield.co.uk"
            };
            Customers.Add(customer11);

            var customer12 = new Customer
            {
                CompanyName = "Fenland Airfield",
                ContactName = "Joel Vandyke",
                ContactNumber = "077 5175 8110",
                Address = GetRandomAddress(),
                Email = "joel@fenlandairfield.co.uk",
                Website = "http://www.fenlandairfield.co.uk"
            };
            Customers.Add(customer12);

            var customer13 = new Customer
            {
                CompanyName = "Elstree Airfield",
                ContactName = "Mario Green",
                ContactNumber = "078 0352 6544",
                Address = GetRandomAddress(),
                Email = "mario@elstreeairfield.co.uk",
                Website = "http://www.elstreeairfield.co.uk"
            };
            Customers.Add(customer13);

            var customer14 = new Customer
            {
                CompanyName = "Great Yarmouth Airport",
                ContactName = "Larry Lackey",
                ContactNumber = "079 7448 6326",
                Address = GetRandomAddress(),
                Email = "larry@greatyarmouthairport.co.uk",
                Website = "http://www.greatyarnmouthairport.co.uk"
            };
            Customers.Add(customer14);

            var customer15 = new Customer
            {
                CompanyName = "Seething Airfield",
                ContactName = "Wesley Velazquez",
                ContactNumber = "079 6547 6718",
                Address = GetRandomAddress(),
                Email = "wesley@seethingairfield.co.uk",
                Website = "http://www.seethingairfield.co.uk"
            };
            Customers.Add(customer15);

            var customer16 = new Customer
            {
                CompanyName = "Old Buckenham Airport",
                ContactName = "David Waldrup",
                ContactNumber = "079 5882 6862",
                Address = GetRandomAddress(),
                Email = "david@oldbuckenhamairport.co.uk",
                Website = "http://www.oldbuckenhamairport.co.uk"
            };
            Customers.Add(customer16);

            var customer17 = new Customer
            {
                CompanyName = "Shipdham Airfield",
                ContactName = "Shirley Taylor",
                ContactNumber = "078 0257 7497",
                Address = GetRandomAddress(),
                Email = "shirley@shipdhamairfield.co.uk",
                Website = "shipdhamairfield.co.uk"
            };
            Customers.Add(customer17);

            var customer18 = new Customer
            {
                CompanyName = "London Biggin Hill",
                ContactName = "Terrence Peterson",
                ContactNumber = "078 5164 4585",
                Address = GetRandomAddress(),
                Email = "terreance@londonbigginhill.co.uk",
                Website = "http://www.londonbigginhill.co.uk"
            };
            Customers.Add(customer18);

            var customer19 = new Customer
            {
                CompanyName = "London City",
                ContactName = "Jean Anderson",
                ContactNumber = "077 8255 0633",
                Address = GetRandomAddress(),
                Email = "jean@londoncity.co.uk",
                Website = "http://www.londoncity.co.uk"
            };
            Customers.Add(customer19);

            var customer20 = new Customer
            {
                CompanyName = "Blackpool Airport",
                ContactName = "Jeff Washington",
                ContactNumber = "070 2137 9311",
                Address = GetRandomAddress(),
                Email = "jeff@blackporairport.co.uk",
                Website = "http://www.blackblushairport.co.uk"
            };
            Customers.Add(customer20);

            var customer21 = new Customer
            {
                CompanyName = "Ascot Racecourse Heliport",
                ContactName = "Richard Coleman",
                ContactNumber = "079 4534 7755",
                Address = GetRandomAddress(),
                Email = "richard@ascotracecourse.co.uk",
                Website = "http://www.ascotracecourse.co.uk"
            };
            Customers.Add(customer21);

            var customer22 = new Customer
            {
                CompanyName = "Goodwood Racecourse Heliport",
                ContactName = "Arthur Blevins",
                ContactNumber = "077 6161 0723",
                Address = GetRandomAddress(),
                Email = "arthur@goodwoodracecourse.co.uk",
                Website = "http://www.goodwoodracecourse.co.uk"
            };
            Customers.Add(customer22);

            var customer23 = new Customer
            {
                CompanyName = "Yeovil Aerodrome",
                ContactName = "Ronald Wafer",
                ContactNumber = "077 5474 7537",
                Address = GetRandomAddress(),
                Email = "ronald@yeovilaerodrome.co.uk",
                Website = "http://wwww.yeovilaerodrome.co.uk"
            };
            Customers.Add(customer23);

            var customer24 = new Customer
            {
                CompanyName = "Bagby Airfield",
                ContactName = "Anthony Walden",
                ContactNumber = "079 8143 9468",
                Address = GetRandomAddress(),
                Email = "anthony@bagbyairfield.co.uk",
                Website = "http://www.bagbyairfield.co.uk"
            };
            Customers.Add(customer24);

            var customer25 = new Customer
            {
                CompanyName = "Brough Aerodrome",
                ContactName = "Steven Victor",
                ContactNumber = "078 6999 9646",
                Address = GetRandomAddress(),
                Email = "steven@broughaerodrome.co.uk",
                Website = "http://broughaerodrome.co.uk"
            };
            Customers.Add(customer25);

        }

        Customer GetRandomCustomer()
        {
            Random rnd = new Random();
            int r = rnd.Next(Customers.Count);
            return Customers[r];
        }

        void PopulateEmployees()
        {
            var employee1 = new Employee
            {
                FirstName = "Mike",
                LastName = "James",
                EmailAddress = "mike@contosomaintenance.co.uk",
                CellNumber = "078 6861 8997",
                StartDate = DateTime.Parse("1/1/2018"),
            };
            Employees.Add(employee1);

            var employee2 = new Employee
            {
                FirstName = "Robin-Manuel",
                LastName = "Thiel",
                EmailAddress = "rm@contosomaintenance.co.uk",
                CellNumber = "077 3348 5177",
                StartDate = DateTime.Parse("1/1/2018"),
            };
            Employees.Add(employee2);

            var employee3 = new Employee
            {
                FirstName = "Richard",
                LastName = "Erwin",
                EmailAddress = "rich@contosomaintenance.co.uk",
                CellNumber = "079 0451 3958",
                StartDate = DateTime.Parse("1/1/2018"),
            };
            Employees.Add(employee3);

            var employee4 = new Employee
            {
                FirstName = "Michael",
                LastName = "Sivers",
                EmailAddress = "michael@contosomaintenance.co.uk",
                CellNumber = "077 8893 2019",
                StartDate = DateTime.Parse("1/1/2018"),
            };
            Employees.Add(employee4);
        }

        Employee GetRandomEmployee()
        {
            Random rnd = new Random();
            int r = rnd.Next(Employees.Count);
            return Employees[r];
        }

        void PopulateJobs()
        {
            var job1 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Repair CFM56 on B737-300",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Repair,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
                AssignedTo = GetRandomEmployee(),
            };
            Jobs.Add(job1);

            var job2 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Service B767-600 CFM56 Engine",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
                AssignedTo = GetRandomEmployee(),
            };
            Jobs.Add(job2);

            var job3 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Paint 787",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
                AssignedTo = GetRandomEmployee(),
            };
            Jobs.Add(job3);

            var job4 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Service A318 CFM56 Engine",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
                AssignedTo = GetRandomEmployee(),
            };
            Jobs.Add(job4);

            var job5 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Service DHC8-400 PW150 Engine",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
                AssignedTo = GetRandomEmployee(),
            };
            Jobs.Add(job5);

            var job6 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Repair Rolls-Royce Trent 100",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Repair,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
                AssignedTo = GetRandomEmployee(),
            };
            Jobs.Add(job6);

            var job7 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Service PW4000",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
                AssignedTo = GetRandomEmployee(),
            };
            Jobs.Add(job7);

            var job8 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Repair CF6-80A on B737-200",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Repair,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
                AssignedTo = GetRandomEmployee(),
            };
            Jobs.Add(job8);

            var job9 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Inspect Airframe on 787",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job9);

            var job10 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Install new cooker in 747",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Installation,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job10);

            var job11 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Paint ATR 42",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job11);

            var job12 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Paint CASA C-295",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job12);

            var job13 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Paint Boeing 737",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job13);

            var job14 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Repair composite structure of main door",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job14);

            var job15 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Replace Oil",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job15);

            var job16 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Borescope Examination on A380",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job16);

            var job17 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Borescope Examination of PW127",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job17);

            var job18 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Borescope Examination of CF6",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job18);

            var job19 = new Job
            {
                CreatedAt = GetRandomDate(),
                DueDate = GetRandomDate(false),
                Name = "Fault Isolation on 787 navigation system",
                Details = "Lorem ipsum dolor sit amet, has ei munere dolore, id mel doming assentior. Cu menandri consulatu vis, sea adhuc graece ea. Ius vocent disputando accommodare id. Ei sit porro scribentur, viderer volumus eos te, vocibus commune detraxit est ad. Ei veniam omnesque mediocritatem ius, vix no paulo neglegentur, ne nec civibus maluisset reformidans.",
                Type = JobType.Service,
                Status = JobStatus.Waiting,
                Address = GetRandomAddress(),
            };
            Jobs.Add(job19);

        }

        Job GetRandomJob()
        {
            Random rnd = new Random();
            int r = rnd.Next(Jobs.Count);
            return Jobs[r];
        }

    }
}
