
namespace Tangatek
{
    public abstract class Reference<T>
    {
        public bool UseConstant = true;
        public T ConstantValue;
        public ScriptableVariable<T> Variable;

        public T Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }


    }
}