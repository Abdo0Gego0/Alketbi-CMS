namespace CmsWeb.CustomTagHelpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ActionDisplayAttribute : Attribute
    {

        public string Description { get; }
        public ActionDisplayAttribute(string description)
        {
            Description = description;
        }
    }
}
