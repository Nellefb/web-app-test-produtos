namespace web_app_domain
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int Quant_estoque { get; set; }
        public DateTime Data_criacao { get; set; } = DateTime.Now;
    }
}

//CREATE TABLE `sys`.`produtos` (
//  `Id` INT NOT NULL AUTO_INCREMENT,
//  `Nome` VARCHAR(45) NULL,
//  `Preco` DOUBLE NULL,
//  `Quant_estoque` INT NULL,
//  `Data_criacao` DATETIME NULL,
//  PRIMARY KEY (`Id`));