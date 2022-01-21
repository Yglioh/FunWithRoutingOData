using System;

namespace Models.V1
{
    /// <summary>
    /// Non-generic representation of <see cref="ProcesFlowParameter{T}"/>
    /// </summary>
    public class Parameter
    {
        public Parameter()
        {
        }

        public Parameter(ProcessFlowParameterType type)
        {
            Type = type;
        }

        public int Id { get; set; }
        public string Label { get; set; }
        public ProcessFlowParameterType Type { get; set; }
    }

    /// <summary>
    /// Example of an entity that contains a generic typed property.
    /// Example of second/third level contained entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ProcesFlowParameter<T> : Parameter
    {
        protected ProcesFlowParameter()
        {
        }

        protected ProcesFlowParameter(ProcessFlowParameterType type)
            : base(type)
        {
        }

        public T DefaultValue { get; set; }
    }

    /// <summary>
    /// <see cref="string"/> implementation of <see cref="ProcesFlowParameter{T}"/>
    /// </summary>
    public class TextParameter : ProcesFlowParameter<string>
    {
        public TextParameter()
            : base(ProcessFlowParameterType.Text)
        {
        }
    }

    /// <summary>
    /// <see cref="int"/> implementation of <see cref="ProcesFlowParameter{T}"/>
    /// </summary>
    public class NumberParameter : ProcesFlowParameter<int>
    {
        public NumberParameter()
            : base(ProcessFlowParameterType.Number)
        {
        }
    }

    /// <summary>
    /// <see cref="bool"/> implementation of <see cref="ProcesFlowParameter{T}"/>
    /// </summary>
    public class CheckBoxParameter : ProcesFlowParameter<bool>
    {
        public CheckBoxParameter()
            : base(ProcessFlowParameterType.ChechBox)
        {
        }
    }

    /// <summary>
    /// <see cref="DateTimeOffset"/> implementation of <see cref="ProcesFlowParameter{T}"/>
    /// </summary>
    public class DateParameter : ProcesFlowParameter<DateTimeOffset>
    {
        public DateParameter()
            : base(ProcessFlowParameterType.Date)
        {
        }
    }

    public enum ProcessFlowParameterType
    {
        Text,
        Number,
        Date,
        ChechBox,
        Entity,
    }
}
