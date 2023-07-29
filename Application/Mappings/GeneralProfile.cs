using Application.DTOs;
using Application.Features.Commands.CreateClientCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            #region Commands
            CreateMap<CreateClientCommand, Client>();
            #endregion

            #region DTos
            CreateMap<Client, ClientDto>();
            #endregion
        }
    }
}
