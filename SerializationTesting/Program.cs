using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var locations = GetLocationData();


            BinaryTest(locations);
            var testing = BinaryDeserialise();

            foreach (var loc in testing)
            {
                string parentName = string.Empty;
                if (loc.Parent == null)
                {
                    parentName = "Root";
                }
                else
                {
                    parentName = loc.Parent.Name;
                }
                Console.WriteLine($"Location: {loc.Name} Parent: {parentName}");
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

        private static void BinaryTest(List<Location> locations)
        {
            using (var fs = new FileStream("DataFile.dat", FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, locations);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        private static List<Location> BinaryDeserialise()
        {
            List<Location> deserializedLocations = null;

            // Open the file containing the data that you want to deserialize.
            using (var fs = new FileStream("DataFile.dat", FileMode.Open))
            {
                try
                {
                    var formatter = new BinaryFormatter();

                    // Deserialize the hashtable from the file and 
                    // assign the reference to the local variable.
                    deserializedLocations = (List<Location>)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }


            return deserializedLocations;
        }
    }
}
