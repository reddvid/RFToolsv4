using RFTools.Calculators;
using RFTools.Constants;
using RFTools.Enums;

namespace RFTools.Tests.Unit;

public class CalculatorTests
{
    private readonly SkinDepth _sutSkinDepth = new();
    private readonly StandingWave _sutStandingWaves = new();
    private readonly Resonance _sutResonance = new();

    [Theory]
    [InlineData(0.000001678, 0.999991, 2_400_000_000, 1.3308)]
    [InlineData(0.000001678, 0.999991, 4_000_000_000, 1.0308)]
    [InlineData(0.000001678, 0.999991, 5_000_000_000, 0.922)]
    public void CalculateSkinDepth_ReturnResult_SameValue(
        double resistivity,
        double permeability,
        double frequency,
        decimal expected)
    {
        // Act
        var result = _sutSkinDepth.Calculate(resistivity, permeability, frequency);

        // Assert
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void CalculateSkinDepth_ReturnResult_SameUnits()
    {
        // Act
        var result = _sutSkinDepth.Calculate(0.000001678, 0.999991, 2_400_000_000);

        // Assert
        Assert.Equal("µm", result.Unit);
    }

    [Theory]
    [InlineData(Known.Vswr, 5, 5, 0.67, 3.52, 2.55)]
    [InlineData(Known.ReflectionCoefficient, 0.5, 3, 0.5, 6.02, 1.25)]
    [InlineData(Known.ReflectionCoefficient, 0, 1, 0, double.PositiveInfinity, 0)]
    [InlineData(Known.ReturnLoss, 2, 8.72, 0.79, 2, 4.33)]
    public void CalculateStandingWave_ValidInputs_EqualResult(
        Known type,
        double input,
        decimal expectedVswr,
        decimal expectedReflectionCoefficient,
        decimal expectedReturnLoss,
        decimal expectedMismatchLoss)
    {
        // Act
        var result = _sutStandingWaves.Calculate(type, input, precision: 2);
        var actualVswr = result.FirstOrDefault(o => o.Name.Equals("VSWR"))!.Value;
        var actualReflectionCoefficient = result.FirstOrDefault(o => o.Name.Contains("Reflection Coefficient"))!.Value;
        var actualReturnLoss = result.FirstOrDefault(o => o.Name.Equals("Return Loss"))!.Value;
        var actualMismatchLoss = result.FirstOrDefault(o => o.Name.Equals("Mismatch Loss"))!.Value;

        // Assert
        Assert.Equal(expectedVswr, actualVswr);
        Assert.Equal(expectedReflectionCoefficient, actualReflectionCoefficient);
        Assert.Equal(expectedReturnLoss, actualReturnLoss);
        Assert.Equal(expectedMismatchLoss, actualMismatchLoss);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-0.5)]
    public void CalculateStandingWave_InvalidReflectionCoefficient_ThrowArgumentException(double input)
    {
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _sutStandingWaves.Calculate(Known.ReflectionCoefficient, input));
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-0.5)]
    public void CalculateStandingWave_InvalidVSWR_ThrowArgumentException(double input)
    {
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _sutStandingWaves.Calculate(Known.Vswr, input));
    }

    [Fact]
    public void CalculateResonance_FindFrequencyForValues_EqualResult()
    {
        // Act
        var result = _sutResonance.Calculate(Unknown.Frequency, capacitance: 4E-12, inductance: 5E-6);

        // Assert
        Assert.NotStrictEqual(35.588, (double)result.Value);
    }
}