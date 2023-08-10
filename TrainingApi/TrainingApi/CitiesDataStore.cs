using TrainingApi.Model;

namespace TrainingApi
{
    public class CitiesDataStore
    {
        public List<CitiesDto> Cities { get; set; }

        // Singelton pattern
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CitiesDto> {
                new CitiesDto{  Id=1, Name="Hydrabad", Description="A city famous for Biryani.",
                    PlacesToVisit=new List<Places>{
                    new Places{
                    Id=1, Name="Charminar", Description="Good site seeing place."
                    },
                    new Places{ Id=2, Name="Tankbund", Description="Nice lake"}
                    } },
                new CitiesDto{Id=2, Name="Bangalore", Description="Silicon valley",
                PlacesToVisit=new List<Places>{
                new Places{  Id=3, Name="MG Road", Description="Most happening place" }
                , new Places { Id=4, Name="Waterpark", Description="Good place to spend weekend"}
                }
                },
                new CitiesDto{Id=3, Name="Delhi", Description="Capital of India"
                , PlacesToVisit=new List<Places>
                {

                    new Places{ Id=5, Name="Canaut place", Description="CP area"},
                    new Places{Id=6, Name="Red Fort", Description="Indian heritage"}
                }
                }
                };
        }
    }
}
