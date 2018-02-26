//using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SerializationTesting
{
    //TODO:
    // https://stackoverflow.com/questions/6649983/how-to-let-a-parent-class-know-about-a-change-in-its-children

    // TODO:
    // use composite disposable for rx timer..
    // see https://sachabarbs.wordpress.com/2013/05/16/simple-but-nice-state-machine/


    /// <summary>
    /// Location Class, used for storing locations within the home or garden
    /// </summary>
    [Serializable]

    public class Location
    {

        /// <summary>
        /// The occupancy timer
        /// </summary>
        private readonly System.Timers.Timer _occupancyTimer;

        /// <summary>
        /// Prevents a default instance of the <see cref="Location"/> class from being created.
        /// </summary>
        private Location()
        {
            // parameterless constructor for serialization
        }

        private State _state;

        /// <summary>
        /// Resets the timer.
        /// </summary>
        private void ResetTimer()
        {
            Console.WriteLine($"{Name} Occupancy timer restarting");
            _occupancyTimer.Stop();
            _occupancyTimer.Start();
        }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>
        /// The temperature.
        /// </value>
        public double Temperature { get; set; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Location Parent { get; /*set;*/ }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        //public List<ModuleReference> Modules { get; set; }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public List<Location> Children { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is timer running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is timer running; otherwise, <c>false</c>.
        /// </value>
        public bool IsTimerRunning { get; set; }

        /// <summary>
        /// Gets the state of the occupancy.
        /// </summary>
        /// <value>
        /// The state of the occupancy.
        /// </value>
        [JsonIgnore]
        public State OccupancyState
        {
            get => _state;
            set => _state = value;
        }
        //=> _stateMachine.State;

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public Location(Location parent)
        {
            Parent = parent;
            //_children = new ObservableCollection<Location>();
            Children = new List<Location>();
            //  _children.CollectionChanged += CollectionChanged;

            parent?.Children.Add(this);

            _occupancyTimer = new System.Timers.Timer();
            //_stateMachine = CreateStateMachine();
        }

        /// <summary>
        /// Gets all children.
        /// </summary>
        /// <value>
        /// All children.
        /// </value>
        [JsonIgnore]
        public IEnumerable<Location> AllChildren => Children.Union(Children.SelectMany(child => child.AllChildren));

        /// <summary>
        /// Gets a value indicating whether this instance has occupied children.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has occupied children; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool HasOccupiedChildren => AllChildren.Any(child => child.OccupancyState == State.Occupied);

        /// <summary>
        /// Tries the state of the update.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        //public bool TryUpdateState(Trigger trigger)
        //{
        //    if (!_stateMachine.CanFire(trigger))
        //        return false;

        //    _stateMachine.Fire(trigger);
        //    return true;
        //}


        /// <summary>
        /// The reason the state transitioned
        /// TODO testing - this might not be practical
        /// </summary>
        public string TransitionReason { get; set; }
    }
}
