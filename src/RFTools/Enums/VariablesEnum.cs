namespace RFTools.Enums;

public enum Known
{
    Vswr,
    ReflectionCoefficient,
    ReturnLoss
}

public enum Unknown
{
    Frequency,
    Capacitance,
    Inductance,
    Resistance
}

public enum CircuitType
{
    RC,
    RL
}

public enum Conversion
{
    WavelengthToFrequency,
    FrequencyToWavelength
}

public enum Input
{
    dBm,
    mW,
    W,
    dBW,
    Feet,
    Meters,
    Kilometers,
    Miles
}
