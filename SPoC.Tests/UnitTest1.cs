namespace SPoC.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            Redis redis = new Redis();

            OrderService orderService = new OrderService(redis);
            LocationService locationService = new LocationService(redis);
        }
    }

    public class Redis {
        public Dictionary<string, object> Items = new Dictionary<string, object>();
    }

    public class OrderService {

        private readonly Redis redis;

        public OrderService(Redis redis)
        {
            redis = new Redis();
        }
    }

    public class LocationService {
        private readonly Redis redis;

        public LocationService(Redis redis)
        {
            this.redis = redis;
        }
    }

}