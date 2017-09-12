using Common.Layer.CqrsCore;
using Domain.Model.Models.MongoDB;
using System.Collections.Generic;

namespace Business.Commands
{
    public class AddDatasCommand : ICommand
    {
        public AddDatasCommand(List<Data> datas)
        {
            this.Datas = datas;
        }
        public List<Data> Datas { get; private set; }
    }
}
