using AutoMapper;
                //https://www.ezzylearning.net/tutorial/a-step-by-step-guide-of-using-automapper-in-asp-net-Inv

namespace Inv.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
