
using System.Data;
using Dapper;

namespace ProjetoBaseWeb.WebApp.Compartilhado.Infra
{
    public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
    {
        // Ensina o Dapper a SALVAR no banco
        public override void SetValue(IDbDataParameter parameter, TimeOnly value)
        {
            parameter.Value = value.ToTimeSpan(); // Converte para TimeSpan pro SQL entender
            parameter.DbType = DbType.Time;
        }

        // Ensina o Dapper a LER do banco
        public override TimeOnly Parse(object value)
        {
            return TimeOnly.FromTimeSpan((TimeSpan)value); // Converte de volta para TimeOnly
        }
    }
}