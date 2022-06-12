CREATE DATABASE BiblioWebDb;
GO

USE BiblioWebDb;
GO

CREATE TABLE TbUsuario(
Id_Usuario INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Correo NVARCHAR(30) NOT NULL,
Contraseña NVARCHAR(30) NOT NULL
);
GO

CREATE TABLE TbCliente(
Id_Cliente INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre NVARCHAR(30) NOT NULL,
ApellidoPat NVARCHAR(30) NOT NULL,
ApellidoMat NVARCHAR(30) NOT NULL,
Colonia NVARCHAR(30) NULL,
Calle NVARCHAR(30) NULL,
Numero INT NULL,
Id_Usuario INT NOT NULL,

CONSTRAINT fk_Usuario FOREIGN KEY (Id_Usuario) REFERENCES TbUsuario(Id_Usuario)
);
GO

CREATE TABLE TbLibro(
Id_Libro INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Titulo NVARCHAR(50) NOT NULL,
Autor NVARCHAR(50) NOT NULL,
Genero NVARCHAR(50) NOT NULL,
Descripcion NVARCHAR(MAX) NOT NULL,
Cantidad NVARCHAR(3) NOT NULL,
Precio NVARCHAR(20) NOT NULL,
Ruta NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE TbPedido(
Id_Pedido INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Id_Libro INT NOT NULL,
Id_Usuario INT NOT NULL,
Visibilidad BIT NOT NULL,
CONSTRAINT fk_Libro_Pedido FOREIGN KEY (Id_Libro) REFERENCES TbLibro(Id_Libro),
CONSTRAINT fk_Usuario_Pedido FOREIGN KEY (Id_Usuario) REFERENCES TbUsuario(Id_Usuario)

);
GO

CREATE TABLE TbVentas(
Id_Ventas INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Fecha DATETIME NOT NULL,
Id_Pedido INT NOT NULL,

CONSTRAINT fk_Pedido FOREIGN KEY (Id_Pedido) REFERENCES TbPedido(Id_Pedido)
);
GO

INSERT INTO TbLibro VALUES ('El Libro de Mormón','Mormón','Suspenso Horror','Lorem ipsum dolor sit amet consectetur, adipisicing elit. Magni provident optio quibusdam quas ipsum quidem deserunt enim temporibus inventore. Doloribus dolor velit labore sit, perspiciatis molestias magni ullam. Voluptatem voluptas tempore facere minima cumque quos veniam harum, quibusdam fuga similique eos! Accusamus quia voluptas sed maxime autem maiores repellendus nulla molestiae veniam vitae tempore, dolor assumenda rerum sequi incidunt iusto debitis quibusdam necessitatibus nobis? Aperiam nisi neque in, laborum voluptas minima assumenda pariatur, sed cupiditate esse quibusdam cum ad vitae laudantium repellendus eveniet expedita repellat consequuntur, unde odit explicabo sunt. Aliquam veritatis rerum accusamus non iure eius amet placeat quis quibusdam, explicabo dignissimos sit ea iusto eaque et distinctio? Libero minus aspernatur vero dolorem suscipit, numquam officiis quibusdam repudiandae quae cumque est illum deleniti aliquid voluptatum fugit in quis eos nostrum. Laborum dolore soluta quis, debitis ad, facilis nesciunt ipsum rerum qui exercitationem eaque sed officia? Velit totam laboriosam, placeat perspiciatis laudantium tempore autem esse pariatur voluptatem, optio suscipit aperiam voluptatum? Ipsum repudiandae, dignissimos placeat officiis quia non? Dicta porro, unde nemo voluptate sed culpa dolorem quasi. Voluptate necessitatibus aspernatur rem consequatur unde minus laudantium iste assumenda libero dolor cumque ea molestiae reprehenderit quo, rerum laborum itaque, deserunt ab neque odit earum. Quos ipsum ullam vel amet inventore impedit corporis beatae autem, unde veniam facilis cum quisquam exercitationem velit officia itaque sit obcaecati, eveniet fugiat, corrupti necessitatibus esse id? In, similique! Laborum suscipit optio corporis error debitis ducimus harum esse officiis adipisci rerum temporibus vitae, soluta, veritatis impedit maiores dolor dolorem repellat magnam perferendis neque obcaecati inventore? Ipsum pariatur natus libero ad non consequuntur quia veritatis numquam doloremque cumque sit eaque officia dicta, quo veniam voluptate voluptas neque nam tempore sint corporis minima ullam. Accusantium ipsam veniam, minima recusandae aspernatur vero soluta id iste incidunt reiciendis temporibus nobis assumenda repellat dignissimos, nisi sapiente provident at eius corrupti numquam facere, veritatis ea aut? Amet dignissimos commodi quaerat ratione possimus accusantium ipsa nihil nulla deleniti earum consequatur, mollitia cupiditate in tenetur cum. Tempora, provident quaerat velit a quidem deleniti inventore consectetur labore cupiditate autem! Voluptatem provident illum fuga ad commodi velit facere tempora nulla impedit obcaecati, quia praesentium modi iure consectetur facilis deserunt odio repudiandae quis mollitia nihil consequatur beatae fugit nobis exercitationem. Error, architecto et vel nisi quidem maxime quas nihil, explicabo eligendi doloribus eos accusamus veritatis vitae nam adipisci, recusandae hic excepturi laboriosam! Quo velit et aliquid cumque deserunt voluptatum amet? Fuga ratione adipisci laboriosam maxime atque, incidunt necessitatibus architecto. Repudiandae natus, sit officia, laborum veritatis placeat, incidunt vel voluptate dignissimos unde quidem odit dicta quos omnis nulla doloremque iusto nostrum nam. Aut ducimus saepe amet, distinctio velit culpa. Placeat doloremque itaque sunt minima quas magni explicabo numquam suscipit, quibusdam, accusamus impedit ratione voluptates! Voluptas eveniet veniam, deserunt cumque quis et fuga similique, tempore magni commodi delectus assumenda exercitationem pariatur eligendi nostrum. Fugit nam est incidunt deleniti autem mollitia pariatur! Nisi explicabo alias in accusamus, at cum voluptatum dolor quisquam quo eum ipsum totam temporibus omnis quis velit qui libero?','100','$500.00','/img/Libro.png');
INSERT INTO TbLibro VALUES ('Aventuras de Bill','Bill','Infantil','Lorem ipsum dolor sit amet consectetur, adipisicing elit. Magni provident optio quibusdam quas ipsum quidem deserunt enim temporibus inventore. Doloribus dolor velit labore sit, perspiciatis molestias magni ullam. Voluptatem voluptas tempore facere minima cumque quos veniam harum, quibusdam fuga similique eos! Accusamus quia voluptas sed maxime autem maiores repellendus nulla molestiae veniam vitae tempore, dolor assumenda rerum sequi incidunt iusto debitis quibusdam necessitatibus nobis? Aperiam nisi neque in, laborum voluptas minima assumenda pariatur, sed cupiditate esse quibusdam cum ad vitae laudantium repellendus eveniet expedita repellat consequuntur, unde odit explicabo sunt. Aliquam veritatis rerum accusamus non iure eius amet placeat quis quibusdam, explicabo dignissimos sit ea iusto eaque et distinctio? Libero minus aspernatur vero dolorem suscipit, numquam officiis quibusdam repudiandae quae cumque est illum deleniti aliquid voluptatum fugit in quis eos nostrum. Laborum dolore soluta quis, debitis ad, facilis nesciunt ipsum rerum qui exercitationem eaque sed officia? Velit totam laboriosam, placeat perspiciatis laudantium tempore autem esse pariatur voluptatem, optio suscipit aperiam voluptatum? Ipsum repudiandae, dignissimos placeat officiis quia non? Dicta porro, unde nemo voluptate sed culpa dolorem quasi. Voluptate necessitatibus aspernatur rem consequatur unde minus laudantium iste assumenda libero dolor cumque ea molestiae reprehenderit quo, rerum laborum itaque, deserunt ab neque odit earum. Quos ipsum ullam vel amet inventore impedit corporis beatae autem, unde veniam facilis cum quisquam exercitationem velit officia itaque sit obcaecati, eveniet fugiat, corrupti necessitatibus esse id? In, similique! Laborum suscipit optio corporis error debitis ducimus harum esse officiis adipisci rerum temporibus vitae, soluta, veritatis impedit maiores dolor dolorem repellat magnam perferendis neque obcaecati inventore? Ipsum pariatur natus libero ad non consequuntur quia veritatis numquam doloremque cumque sit eaque officia dicta, quo veniam voluptate voluptas neque nam tempore sint corporis minima ullam. Accusantium ipsam veniam, minima recusandae aspernatur vero soluta id iste incidunt reiciendis temporibus nobis assumenda repellat dignissimos, nisi sapiente provident at eius corrupti numquam facere, veritatis ea aut? Amet dignissimos commodi quaerat ratione possimus accusantium ipsa nihil nulla deleniti earum consequatur, mollitia cupiditate in tenetur cum. Tempora, provident quaerat velit a quidem deleniti inventore consectetur labore cupiditate autem! Voluptatem provident illum fuga ad commodi velit facere tempora nulla impedit obcaecati, quia praesentium modi iure consectetur facilis deserunt odio repudiandae quis mollitia nihil consequatur beatae fugit nobis exercitationem. Error, architecto et vel nisi quidem maxime quas nihil, explicabo eligendi doloribus eos accusamus veritatis vitae nam adipisci, recusandae hic excepturi laboriosam! Quo velit et aliquid cumque deserunt voluptatum amet? Fuga ratione adipisci laboriosam maxime atque, incidunt necessitatibus architecto. Repudiandae natus, sit officia, laborum veritatis placeat, incidunt vel voluptate dignissimos unde quidem odit dicta quos omnis nulla doloremque iusto nostrum nam. Aut ducimus saepe amet, distinctio velit culpa. Placeat doloremque itaque sunt minima quas magni explicabo numquam suscipit, quibusdam, accusamus impedit ratione voluptates! Voluptas eveniet veniam, deserunt cumque quis et fuga similique, tempore magni commodi delectus assumenda exercitationem pariatur eligendi nostrum. Fugit nam est incidunt deleniti autem mollitia pariatur! Nisi explicabo alias in accusamus, at cum voluptatum dolor quisquam quo eum ipsum totam temporibus omnis quis velit qui libero?','100','$300.00','/img/ImagenPrueba.png');

SELECT * FROM TbUsuario

SELECT * FROM TbVentas

Select * from TbPedido