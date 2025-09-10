using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDesginPattern.Strategy
{
    internal class SolutionComposition
    {
    }
    /*
     Keep out the things that are not fixed/ not fixed behaviour
     keep things together which are fixed.
     
    in Robot  - Talk,Walk,Fly are dynamic 
              - Projection Fixed (all robot have this function)
     
    make Talk,Walk and Fly one abstract class

    and inhert the different behaviour
     Talk -  Can Talk , Can;t Talk
     Walk  - Can Walk ,Can't walk ......

    if tomorrow new behaviours come then create a new abstract class for that
    
    now base robot class one  method projection and has a object of abstract class of Fly,Walk,Talk

    Robot             
     -IFlyable           ------------> IFlyable (Fly())-----> CanFly and NotFly
     -ITalkable
     -IWalkabel

    Fly() -> Delegate method ->call the concerete method of IFlyable class
    Talk() -> Delegate method -> Same
    Walk() -> Delegate Method -> Same no other logic just delegation
      
    2 +abstract Projection()
          |          |
          |          |
    Companion R    Worker R
     -Projection() -Projection()

    Robot class has-a relation ship with Different Behaviour

    Robot robot=new CompanionR(
    new Fly(),
    new NotWalk(),
    new Talk());

    robot.
    At runtime we decide which type of beahiour we need to add in robot.

    We totaly removed the ingertiance and moved to compostion

    --------------
    After this stage it looks like

    Robot                        IFlyable         IWalkable          ITalkable
    ------------         ---------------------  -------------    ------------------
      IFlybale f                  +fly()           +walk()             +talk()
      IWalkable w             Fly       NoFly    Walk     NoWalk    Talk    NotTalk
      ITakable t
      
    Fly()-> f.fly()
    Walk()-> w.walk()
    Talk()-> t.talk()

    +abstract Projection()
    ---------------------------
          |          |
          |          |
    Companion R    Worker R
     -Projection() -Projection()


    Here we have inheritance in one place say Companion R and Worker R so we can remove this and add a interface that act for 
    projection

    Robot                        IFlyable         IWalkable          ITalkable              IProjectable
    ------------         ---------------------  -------------    ------------------     -----------------
      IFlybale f                  +fly()           +walk()             +talk()             +Projection
      IWalkable w             Fly       NoFly    Walk     NoWalk    Talk    NotTalk         Project
      ITakable t
      IProjectbale p
      
    Fly()-> f.fly()
    Walk()-> w.walk()
    Talk()-> t.talk()
    Project() -> p.project()

    Execute()-> encapsualte Fly(),Walk(),Talk(),Projection()
    
    Client
    
    Robot companion = new Robot(new Fly(),new NoWalk(),new Talk(),new Projection)
    companion.Execute();


     */
}
