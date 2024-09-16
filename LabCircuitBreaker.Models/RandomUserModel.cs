namespace LabCircuitBreaker.Models
{
    public class ResultRandomUserModel
    {
        public List<RandomUserModel> Results { get; set; }
    }

    public class RandomUserModel
    {
        public string Gender { get; set; }
        public RandomUserNameModel Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public RandomUserLocationModel Location { get; set; }
        public RandomUserPictureModel Picture { get; set; }
    }

    public class RandomUserNameModel
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class RandomUserLocationModel
    {
        public RandomUserLocationStreetModel Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class RandomUserLocationStreetModel
    {
        public string Number { get; set; }
        public string Name { get; set; }
    }

    public class RandomUserPictureModel
    {
        public string Large { get; set; }
        public string Medium { get; set; }
    }

}
