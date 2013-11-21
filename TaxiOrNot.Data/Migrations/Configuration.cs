namespace TaxiOrNot.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaxiOrNot.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<TaxiOrNot.Data.TaxiOrNotDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        private static Random rand = new Random();

        private string[] descriptions =
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec euismod elementum accumsan. Pellentesque nulla neque, ullamcorper in nisl et, hendrerit consequat dolor. Sed pellentesque nunc et pretium venenatis. Mauris volutpat erat eu sem pharetra, at auctor nunc porttitor. Cras ullamcorper placerat ornare. Proin blandit est at congue fringilla. Proin aliquam tincidunt nulla sit amet aliquet. Nulla magna lorem, euismod eu venenatis vel, eleifend eu purus. Nunc et ligula nunc. Proin imperdiet quam at accumsan scelerisque. Ut et ligula a turpis dapibus congue vel eget turpis. Fusce rutrum diam eu quam imperdiet vehicula. Curabitur ut sapien ornare, pellentesque ante sit amet, commodo erat. Nam malesuada urna ac lorem sagittis rhoncus.",
            "Duis vel interdum mi. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque quis tellus ut sem cursus ornare. In vel rutrum sem. Etiam fermentum et augue at bibendum. Sed sed tortor ac nisi viverra scelerisque a et nunc. Praesent et ante vel massa lobortis suscipit non ut lectus. Nulla semper erat massa, sit amet tristique est hendrerit vel. Ut elementum ornare purus, eu porttitor ante lobortis ut. Vestibulum in tincidunt neque.",
            "Pellentesque fringilla vulputate augue, at sagittis enim rhoncus tempor. In fermentum, sem a tempor imperdiet, sapien eros accumsan quam, a ultrices sapien ipsum ac felis. Cras a velit quis mi rutrum tempus eu non lorem. In hac habitasse platea dictumst. Vestibulum vel nibh fermentum, adipiscing mauris ornare, ornare arcu. Donec sollicitudin feugiat molestie. Praesent non facilisis erat. Donec suscipit faucibus felis vel gravida. Aliquam ullamcorper a urna sit amet pretium. Vestibulum velit libero, suscipit eu placerat a, sagittis ac nunc. Cras enim purus, dictum a eleifend vitae, vulputate et libero. Vivamus sit amet lacus non justo rhoncus congue in at tellus. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
            "Nulla accumsan suscipit dui id aliquam. In ut interdum enim, at rhoncus nunc. Mauris adipiscing mollis suscipit. Fusce ut dui vitae massa ultrices aliquam. Vivamus sit amet vestibulum turpis. Pellentesque justo orci, consequat et velit rhoncus, pulvinar vestibulum sapien. Suspendisse potenti. Maecenas tincidunt viverra ligula eget porttitor. Etiam nec feugiat orci. Nulla id consequat diam, gravida venenatis nunc.",
            "Vestibulum placerat ultricies nisl, non lobortis odio vehicula ac. Phasellus fermentum neque lorem, eget tempus ligula commodo ut. Morbi eu sodales urna. Donec luctus felis elit, nec faucibus ipsum auctor eu. Sed risus lacus, gravida ac nulla nec, posuere pellentesque nibh. Pellentesque accumsan ligula libero, non lobortis lorem congue sed. Mauris fermentum est ornare enim auctor, sed consectetur purus molestie. Aliquam vel adipiscing nibh. Vestibulum ac odio vel nisl commodo eleifend eu non tortor. Donec varius ipsum et enim elementum elementum. Duis vestibulum neque in elit eleifend pellentesque. Donec blandit vel libero vitae rhoncus. In tempor dui arcu, auctor vestibulum arcu iaculis ac. Proin ornare nec neque et facilisis. Cras ultrices elit nec interdum consequat. Nam metus mi, fringilla eu quam vel, consequat scelerisque ipsum.",
            "In sagittis sem non magna consequat, a eleifend dui elementum. Vestibulum lectus tellus, dignissim id pellentesque ac, rhoncus a massa. Sed porta felis metus, in adipiscing metus commodo sed. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc sed ipsum tincidunt, sodales diam vel, hendrerit orci. Ut malesuada suscipit dictum. In urna eros, hendrerit sit amet eros vitae, eleifend pretium lorem. Proin placerat mauris id dignissim mattis. Integer malesuada facilisis lectus vel pellentesque. Quisque fringilla sodales velit at aliquam. Fusce quis massa vel metus sodales gravida et eu justo. Morbi suscipit lacinia laoreet. Aenean vel facilisis nulla. Proin in ullamcorper velit. Suspendisse bibendum vel erat eget convallis."
        };

        protected override void Seed(TaxiOrNot.Data.TaxiOrNotDbContext context)
        {
            SeedVoteTypes(context);
            SeedCities(context);
        }

        private void SeedCities(TaxiOrNotDbContext context)
        {
            City[] initialCities =
            {
                new City()
                {
                    Name = "Burgas",
                    Country = "Bulgaria",
                    Latitude = 42.497678M,
                    Longitude = 27.470025M
                },
                new City()
                {
                    Name = "Sofia",
                    Country = "Bulgaria",
                    Latitude = 42.697626M,
                    Longitude = 23.322284M
                },
                new City()
                {
                    Name = "Plovdiv",
                    Country = "Bulgaria",
                    Latitude = 42.143737M,
                    Longitude = 24.749455M
                },
                new City()
                {
                    Name = "Varna",
                    Country = "Bulgaria",
                    Latitude = 43.21686M,
                    Longitude = 27.911364M
                }
            };

            foreach (var initialCity in initialCities)
            {
                if (!context.Cities.Any(c => c.Name.ToLower() == initialCity.Name.ToLower() &&
                                             c.Country.ToLower() == initialCity.Country.ToLower()))
                {
                    context.Cities.Add(initialCity);
                }
            }
            context.SaveChanges();
        }

        private void SeedTaxis(TaxiOrNotDbContext context)
        {
            foreach (var city in context.Cities)
            {
                for (int i = 0; i < 10; i++)
                {
                    city.Taxis.Add(new Taxi()
                    {
                        Name = string.Format("{0} #{1}", city.Name, i),
                        Description = descriptions[rand.Next(descriptions.Length)],
                        DailyBookingFare = 0.70M,
                        DailyInitialFare = 0.7M,
                        DailyKmFare = 0.20M,
                        DailyMinFare = 0.30M,
                        NightlyBookingFare = 0.70M,
                        NightlyInitialFare = 0.7M,
                        NightlyKmFare = 0.20M,
                        NightlyMinFare = 0.30M,
                        Telephone="0877137528",
                        WebSite = "http://minkov.it"
                    });
                }
            }
            context.SaveChanges();
        }

        private void SeedVoteTypes(TaxiOrNotDbContext context)
        {
            if (!context.VoteTypes.Any(vt => vt.Type == "liked"))
            {
                context.VoteTypes.Add(new EntityModels.VoteType()
                {
                    Type = "liked"
                });
                context.SaveChanges();
            }
            if (!context.VoteTypes.Any(vt => vt.Type == "disliked"))
            {
                context.VoteTypes.Add(new EntityModels.VoteType()
                {
                    Type = "disliked"
                });
                context.SaveChanges();
            }
        }
    }
}