namespace Engine.Graphics.Scheduler
{
    public class GlFuncResult<T> : GlCallResult
    {
        internal T _value;

        public GlFuncResult(GlCall targetGlCall, bool synchronised) : base(targetGlCall, synchronised)
        {
        }

        public T Value
        {
            get
            {
                _signal.Wait();
                return _value;
            }
        }
    }
}