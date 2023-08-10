namespace TrainingApi.Model
{
    public class CitiesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int NumberOfPlacesToVisit { get { return PlacesToVisit.Count; } }
        public ICollection<Places> PlacesToVisit { get; set; }=new List<Places>();
    }
}
