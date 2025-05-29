using System;
using System.Collections.Generic;

namespace DesignPatterns.Homework
{
    // This homework is based on the Observer Pattern (https://learn.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern)
    // You will implement a weather monitoring system using the Observer pattern

    #region Observer Pattern Interfaces

    // The subject interface that all weather stations must implement
    public interface IWeatherStation
    {
        // Methods to manage observers
        void RegisterObserver(IWeatherObserver observer);
        void RemoveObserver(IWeatherObserver observer);
        void NotifyObservers();

        // Weather data properties
        float Temperature { get; }
        float Humidity { get; }
        float Pressure { get; }
    }

    // The observer interface that all display devices must implement
    public interface IWeatherObserver
    {
        void Update(float temperature, float humidity, float pressure);
    }

    #endregion

    #region Weather Station Implementation

    // Concrete implementation of a weather station
    public class WeatherStation : IWeatherStation
    {
        // List to store all registered observers
        private List<IWeatherObserver> _observers;

        // Weather data
        private float _temperature;
        private float _humidity;
        private float _pressure;

        // Constructor
        public WeatherStation()
        {
            _observers = new List<IWeatherObserver>();
        }

        // Methods to register and remove observers
        public void RegisterObserver(IWeatherObserver observer)
        {
            // TODO: Implement RegisterObserver method
            // 1. Add the observer to the _observers list
            // 2. Print a message indicating that an observer has been registered
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer), "Observer cannot be null.");
            }
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"Observer {observer.GetType().Name} registered.");
            }
            else
            {
                Console.WriteLine($"Observer {observer.GetType().Name} is already registered.");
            }
        }

        public void RemoveObserver(IWeatherObserver observer)
        {
            // TODO: Implement RemoveObserver method
            // 1. Remove the observer from the _observers list
            // 2. Print a message indicating that an observer has been removed
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer), "Observer cannot be null.");
            }
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
                Console.WriteLine($"Observer {observer.GetType().Name} removed.");
            }
            else
            {
                Console.WriteLine($"Observer {observer.GetType().Name} is not registered.");
            }
        }

        // Method to notify all observers when weather data changes
        public void NotifyObservers()
        {
            // TODO: Implement NotifyObservers method
            // 1. Loop through each observer in the _observers list
            // 2. Call the Update method on each observer, passing the current weather data
            // 3. Print a message indicating that observers are being notified
            Console.WriteLine("\nNotifying observers of weather data change...");
            foreach (var observer in _observers)
            {
                observer.Update(_temperature, _humidity, _pressure);
                Console.WriteLine($"Observer {observer.GetType().Name} notified.");
            }
        }

        // Properties to access weather data
        public float Temperature => _temperature;
        public float Humidity => _humidity;
        public float Pressure => _pressure;

        // Method to update weather data and notify observers
        public void SetMeasurements(float temperature, float humidity, float pressure)
        {
            Console.WriteLine("\n--- Weather Station: Weather measurements updated ---");

            // Update weather data
            _temperature = temperature;
            _humidity = humidity;
            _pressure = pressure;

            Console.WriteLine($"Temperature: {_temperature}°C");
            Console.WriteLine($"Humidity: {_humidity}%");
            Console.WriteLine($"Pressure: {_pressure} hPa");

            // Notify observers of the change
            NotifyObservers();
        }
    }

    #endregion

    #region Observer Implementations

    // TODO: Implement CurrentConditionsDisplay class that implements IWeatherObserver
    // 1. The class should have temperature, humidity, and pressure fields
    // 2. Implement the Update method to update these fields when notified
    // 3. Implement a Display method to show the current conditions
    // 4. The constructor should accept an IWeatherStation and register itself with that station
    public class CurrentConditionsDisplay : IWeatherObserver
    {
        private float _temperature;
        private float _humidity;
        private float _pressure;
        private readonly IWeatherStation _weatherStation;
        public CurrentConditionsDisplay(IWeatherStation weatherStation)
        {
            _weatherStation = weatherStation ?? throw new ArgumentNullException(nameof(weatherStation));
            _weatherStation.RegisterObserver(this);
        }
        public void Update(float temperature, float humidity, float pressure)
        {
            _temperature = temperature;
            _humidity = humidity;
            _pressure = pressure;
        }
        public void Display()
        {
            Console.WriteLine("\n--- Current Conditions ---");
            Console.WriteLine($"Temperature: {_temperature}°C");
            Console.WriteLine($"Humidity: {_humidity}%");
            Console.WriteLine($"Pressure: {_pressure} hPa");
        }
    }

    // TODO: Implement StatisticsDisplay class that implements IWeatherObserver
    // 1. The class should track min, max, and average temperature
    // 2. Implement the Update method to update these statistics when notified
    // 3. Implement a Display method to show the temperature statistics
    // 4. The constructor should accept an IWeatherStation and register itself with that station
    public class StatisticsDisplay : IWeatherObserver
    {
        private float _minTemperature = float.MaxValue;
        private float _maxTemperature = float.MinValue;
        private float _AVGTemperature = 0f;
        private readonly IWeatherStation _weatherStation;

        public void Update(float temperature, float humidity, float pressure)
        {
            // Update min, max, and average temperature
            if (temperature < _minTemperature)
            {
                _minTemperature = temperature;
            }
            if (temperature > _maxTemperature)
            {
                _maxTemperature = temperature;
            }
            _AVGTemperature = (_AVGTemperature + temperature) / 2;

        }
        public void Display()
        {
            Console.WriteLine("\n--- Temperature Statistics ---");
            Console.WriteLine($"Min Temperature: {_minTemperature}°C");
            Console.WriteLine($"Max Temperature: {_maxTemperature}°C");
            Console.WriteLine($"Average Temperature: {_AVGTemperature}°C");
        }
        public StatisticsDisplay(IWeatherStation weatherStation)
        {
            _weatherStation = weatherStation ?? throw new ArgumentNullException(nameof(weatherStation));
            _weatherStation.RegisterObserver(this);
        }
    }

    // TODO: Implement ForecastDisplay class that implements IWeatherObserver
    // 1. The class should track the last pressure reading to predict the forecast
    // 2. Implement the Update method to update the forecast when notified
    // 3. Implement a Display method to show the weather forecast
    // 4. The constructor should accept an IWeatherStation and register itself with that station
    // 5. The forecast logic can be simple: if pressure is rising -> "Improving weather",
    //    if it's falling -> "Cooler, rainy weather", if no change -> "Same weather"
    public class ForecastDisplay : IWeatherObserver
    {
        private float _lastPressure;
        private string? forecast;
        private readonly IWeatherStation _weatherStation;
        public ForecastDisplay(IWeatherStation weatherStation)
        {
            _weatherStation = weatherStation ?? throw new ArgumentNullException(nameof(weatherStation));
            _weatherStation.RegisterObserver(this);
            _lastPressure = weatherStation.Pressure; // Initialize with the current pressure
        }
        public void Update(float temperature, float humidity, float pressure)
        {
            if (pressure > _lastPressure)
            {
                forecast = "Improving weather";
            }
            else if (pressure < _lastPressure)
            {
                forecast = "Cooler, rainy weather";
            }
            else
            {
                forecast = "Same weather";
            }
            _lastPressure = pressure; // Update last pressure for next forecast
        }
        public void Display()
        { 
            Console.WriteLine("\n--- Weather Forecast ---");
            Console.WriteLine($"Forecast: {forecast}");
        }
    }

    #endregion

    #region Application

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Observer Pattern Homework - Weather Station\n");

            try
            {
                // Create the weather station (subject)
                WeatherStation weatherStation = new WeatherStation();

                // Create display devices (observers)
                Console.WriteLine("Creating display devices...");

                // TODO: Uncomment these lines after implementing the required classes
                CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherStation);
                StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherStation);
                ForecastDisplay forecastDisplay = new ForecastDisplay(weatherStation);

                // Simulate weather changes
                Console.WriteLine("\nSimulating weather changes...");

                // Initial weather
                weatherStation.SetMeasurements(25.2f, 65.3f, 1013.1f);

                // Display information from all displays
                Console.WriteLine("\n--- Displaying Information ---");
                currentDisplay.Display();
                statisticsDisplay.Display();
                forecastDisplay.Display();

                // Weather change 1
                weatherStation.SetMeasurements(28.5f, 70.2f, 1012.5f);

                // Display updated information
                Console.WriteLine("\n--- Displaying Updated Information ---");
                currentDisplay.Display();
                statisticsDisplay.Display();
                forecastDisplay.Display();

                // Weather change 2
                weatherStation.SetMeasurements(22.1f, 90.7f, 1009.2f);

                // Display updated information again
                Console.WriteLine("\n--- Displaying Final Information ---");
                currentDisplay.Display();
                statisticsDisplay.Display();
                forecastDisplay.Display();

                // Test removing an observer
                Console.WriteLine("\nRemoving CurrentConditionsDisplay...");
                 weatherStation.RemoveObserver(currentDisplay);

                // Weather change after removing an observer
                weatherStation.SetMeasurements(24.5f, 80.1f, 1010.3f);

                // Display information without the removed observer
                Console.WriteLine("\n--- Displaying Information After Removal ---");
                statisticsDisplay.Display();
                forecastDisplay.Display();

                Console.WriteLine("\nObserver Pattern demonstration complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    #endregion
}