namespace MasterDesginPattern.Strategy
{
    internal class BadInhertianceExample
    {
    }

    /* We have a Robot which can
     *  -Walk (Different)
     *  -Talk  (Different)
     *  -Projection (Same)
     * 
     * Which have to type of Robot 
     *  - Companion R 
     *     Projection
     *  - Wroker R
     *    -Projection 
     *  
     *  --Now 3rd type of Robot say Sparrow R  
     *    -Fly
     *    -Projection
     *   
     *  --One more type say Crow R
     *    -Fly
     *    -Projection
     * 
     *  Sparrow and Crow both can fly and have same implemention
     *  which is Break the principle of DRY(Duplicate)
     *  
     *  --We have create on base class say Flyable Robot
     *                    Flybale Robot -Fly()
     *    Sparrow R (Projection)        Crow R  (Projection)
     *    
     *   -- This hierachy keep on growing 
     *      Problem with Inhertiance
     *        -Code Reuse
     *        -To add new feature a lot of changes we require 
     *        -Break OCP
     *        
     *    
     *    
     *  
     *  
     *  
     *  
     *  
           
     
     */


    public abstract class BadRobot
    {
        public void BadWalk()
        {

        }

        public void BadTalk()
        {

        }

        public abstract void Projection();
    }

    public abstract class BadFlyRobot : BadRobot
    {
        public void BadFly()
        {

        }
    }

    public class BadCompanionRobot : BadFlyRobot
    {
        public override void Projection()
        {
            throw new NotImplementedException();
        }
    }

    public class BadWorkerRobot: BadFlyRobot
    {
        public override void Projection()
        {
            throw new NotImplementedException();
        }
    }

    public class BadSparrowR :BadFlyRobot
    {
        public override void Projection()
        {
            throw new NotImplementedException();
        }
    }

    public class BadCrowR:BadFlyRobot
    {
        public override void Projection()
        {
            throw new NotImplementedException();
        }
    }
}
