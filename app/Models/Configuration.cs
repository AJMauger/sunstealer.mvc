namespace sunstealer.mvc.Models
{
    public class Configuration : System.Collections.Generic.Dictionary<string, object>
    {
        /*
{
  "array": [
    1,
    2,
    3
  ],
  "number": 6,
  "string": "six"
}        
         */

        public Configuration()
        {
            int[] array = { 1, 2, 3};
            this.Add("array", array);
            this.Add("number", 6);
            this.Add("string", "six");
        }

        public bool Validate(Models.Configuration configuration)
        {
            return true;
        }
    }
}
