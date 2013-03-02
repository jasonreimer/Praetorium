using Praetorium.Logging;
using Praetorium.Properties;
using Praetorium.Services;
using System;

namespace Praetorium
{
    internal static class Errors
    {
        internal static Exception NoCurrentWcfOperationContext()
        {
            return new InvalidOperationException(Resources.NoCurrentOperationContext);
        }

        internal static Exception NoCurrentHttpContext()
        {
            return new InvalidOperationException(Resources.NoCurrentHttpContext);
        }

        internal static Exception TypeNotServiceContract(string fullTypeName)
        {
            return new TypeNotServiceContractException(string.Format(Resources.TypeNotServiceContract, fullTypeName));
        }

        internal static Exception ChannelFactoriesDisposed()
        {
            return new ObjectDisposedException(Resources.ChannelFactoriesDisposed);
        }

        internal static Exception TypeDoesNotSpecifySectionName()
        {
            return new ArgumentException(Resources.TypeDoesNotSpecifySectionName, "TSection");
        }

        internal static Exception ServiceTypeCacheUnexpectedException(Type serviceContractType, Exception exception)
        {
            return new ServiceTypeCacheException(string.Format(Resources.ServiceTypeCacheUnexpectedException, serviceContractType.FullName), exception);
        }

        internal static Exception ChannelFactoryWithNoEndpointException(string serviceContractFullName)
        {
            return new ServiceRegistryException(string.Format(Resources.ChannelFactoryWithNoEndpoint, serviceContractFullName));
        }

        internal static Exception ServiceConfigurationNamePropertyIsRequired()
        {
            return new ServiceRegistryException(Resources.ServiceConfigurationNameRequired);
        }

        internal static Exception ChannelFactoryCouldNotBeCreated(Type serviceContractType, string endpointConfigurationName, Exception innerException)
        {
            return new ServiceRegistryException(string.Format(Resources.ChannelFactoryCreateFailed, serviceContractType.FullName, endpointConfigurationName), innerException);
        }

        internal static Exception ExceptionFormatterConfigurationInvalid(string reasons)
        {
            return new ExceptionFormatterConfiguraitonException(string.Format(Resources.ExceptionFormatterConfigurationError, reasons));
        }

        internal static Exception ArgumentNull(string argumentName)
        {
            return new ArgumentNullException(argumentName, string.Format(Resources.ArgumentNullExceptionMessage, argumentName));
        }

        internal static Exception ArgumentNullOrEmpty(string argumentName)
        {
            return new ArgumentException(string.Format(Resources.ArgumentNullOrEmptyExceptionMessage, argumentName), argumentName);
        }

        internal static Exception ArgumentNotDefault(string argumentName)
        {
            return new ArgumentException(string.Format(Resources.ArgumentDefaultExceptionMessage, argumentName), argumentName);
        }

        internal static Exception ArgumentNotNullOrDefault(string argumentName)
        {
            return new ArgumentException(string.Format(Resources.ArgumentNullOrDefaultExceptionMessage, argumentName), argumentName);
        }

        internal static Exception TypeNotSupported(string parameterName, string expectedTypeName)
        {
            return new ArgumentException(string.Format(Resources.TypeNotSupportedExceptionMessage, parameterName, expectedTypeName), parameterName);
        }

        internal static Exception NotAnEnumType(string typeParameterName)
        {
            return new ArgumentException(Resources.NotAnEnumTypeExceptionMessage, typeParameterName);
        }

        internal static Exception InvalidEnumValue(string parameterName, string enumTypeName)
        {
            return new ArgumentException(string.Format(Resources.InvalidEnumValueExceptionMessage, parameterName, enumTypeName), parameterName);
        }

        internal static Exception ReturnValueIsNull(string methodSignature)
        {
            return new PostConditionException(string.Format(Resources.ReturnValueIsNullExceptionMessage, methodSignature));
        }

        internal static Exception ReturnValueIsNullOrEmpty(string methodSignature)
        {
            return new PostConditionException(string.Format(Resources.ReturnValueIsNullOrEmptyExceptionMessage, methodSignature));
        }

        internal static Exception ReturnValueIsTheDefault(string methodSignature)
        {
            return new PostConditionException(string.Format(Resources.ReturnValueIsTheDefaultExceptionMessage, methodSignature));
        }
    }
}
