using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SerializationTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var locations = GetLocationData();


            //Helper.SaveSettings<List<Location>>(locations, "testing.xml");


            string json = JsonConvert.SerializeObject(locations, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });

            Console.WriteLine(json);

            var savedLocations = JsonConvert.DeserializeObject<List<Location>>(json);

            foreach (var loc in savedLocations)
            {
                Console.WriteLine($"Location: {loc.Name} Parent: {loc.Parent}");
            }
            
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        private static List<Location> GetLocationData()
        {
            List<Location> locations = new List<Location>();
            locations.Add(new Location(null) {Name = "Home"});


            locations.Add(new Location(locations.Find(x => x.Name == "Home")) {Name = "House"});
            locations.Add(new Location(locations.Find(x => x.Name == "Home")) {Name = "Garden"});

            locations.Add(new Location(locations.Find(x => x.Name == "Garden")) {Name = "Rear Garden"});
            locations.Add(new Location(locations.Find(x => x.Name == "Garden")) {Name = "Front Garden"});
            locations.Add(new Location(locations.Find(x => x.Name == "Garden")) {Name = "Side Garden"});

            locations.Add(new Location(locations.Find(x => x.Name == "Home")) {Name = "House"});
            locations.Add(new Location(locations.Find(x => x.Name == "House")) {Name = "Upstairs"});
            locations.Add(new Location(locations.Find(x => x.Name == "House")) {Name = "Downstairs"});

            locations.Add(new Location(locations.Find(x => x.Name == "Upstairs")) {Name = "Front Bedroom"});
            locations.Add(new Location(locations.Find(x => x.Name == "Upstairs")) {Name = "Back Bedroom"});
            locations.Add(new Location(locations.Find(x => x.Name == "Upstairs")) {Name = "Office"});
            locations.Add(new Location(locations.Find(x => x.Name == "Upstairs")) {Name = "Bathroom"});
            locations.Add(new Location(locations.Find(x => x.Name == "Upstairs")) {Name = "Upstairs Hall"});

            locations.Add(new Location(locations.Find(x => x.Name == "Downstairs")) {Name = "Kitchen"});
            locations.Add(new Location(locations.Find(x => x.Name == "Downstairs")) {Name = "Downstairs Hall"});
            locations.Add(new Location(locations.Find(x => x.Name == "Downstairs")) {Name = "Front Room"});
            return locations;
        }
    }
}
