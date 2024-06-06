CREATE TABLE WeatherData (
    Id SERIAL PRIMARY KEY,
    Location VARCHAR(50),
    Temperature FLOAT,
    WeatherDescription VARCHAR(100),
    WeatherIcon VARCHAR(10),
    Date TIMESTAMP
);
