using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Career
    {
        public ObjectId Id { get; private set; }

        public string Title { get; private set; }

        public string Summary { get; private set; }

        public string Url { get; private set; }

        public int DurationInMinutes { get; private set; }

        public bool Active { get; private set; }

        public bool Featured { get; private set; }

        public string Tags { get; private set; }

        [BsonElement("_items")]
        public List<CareerItem> Items { get; private set; } = new List<CareerItem>();

        public Career(ObjectId id, string title, string summary, string url, int durationInMinutes, bool active, bool featured, string tags)
        {
            Id = id;
            Title = title;
            Summary = summary;
            Url = url;
            DurationInMinutes = durationInMinutes;
            Active = active;
            Featured = featured;
            Tags = tags;
        }

        public Career(string title, string summary, string url, string tags, bool featured)
        {
            Title = title;
            Summary = summary;
            Url = url;
            Tags = tags;
            Featured = featured;
            Active = true;
            DurationInMinutes = 0;
        }

        public void AddItem(CareerItem item) => Items.Add(item);

        public void Deactivate() => Active = false;
    }
}
