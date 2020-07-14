/* creates a Product sample table and fills it using some adapted product data taken from NopCommerce */

if (exists (select  * 
            from    INFORMATION_SCHEMA.TABLES 
            where   TABLE_SCHEMA = 'dbo' 
            and     TABLE_NAME = 'Products'))
begin
    return
end

create table [dbo].[Products] (
    [Id]            int primary key,
    [Name]          nvarchar(512) not null,
    [ImgUri]        nvarchar(512) not null,
    [Price]         decimal(18, 4) not null,
    [Description]   nvarchar(1024)
)

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (1, N'Digital Storm VANQUISH 3 Custom Performance PC',
        N'digital-storm-vanquish-3-custom-performance-pc-1.png',
        CAST(35073.9500 as decimal(18, 4)),
        N'Digital Storm Vanquish 3 Desktop PC')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (2, N'Lenovo IdeaCentre 600 All-in-One PC',
        N'lenovo-ideacentre-600-all-in-one-pc-1.png',
        CAST(21474.9500 as decimal(18, 4)),
        N'The A600 features a 21.5in screen, DVD or optional Blu-Ray drive, support for the full beans 1920 x 1080 HD, Dolby Home Cinema certification and an optional hybrid analogue/digital TV tuner.')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (3, N'Apple MacBook Pro 13-inch',
        N'apple-macbook-pro-13-inch-1.png',
        CAST(78499.0000 as decimal(18, 4)),
        N'A groundbreaking Retina display. A new force-sensing trackpad. All-flash architecture. Powerful dual-core and quad-core Intel processors. Together, these features take the notebook to a new level of performance. And they will do the same for you in everything you create.')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (4, N'Asus N551JK-XO076H Laptop',
        N'asus-n551jk-xo076h-laptop-1.png',
        CAST(38199.0000 as decimal(18, 4)),
        N'Laptop Asus N551JK Intel Core i7-4710HQ 2.5 GHz, RAM 16GB, HDD 1TB, Video NVidia GTX 850M 4GB, BluRay, 15.6, Full HD, Win 8.1.')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (5, N'Samsung Series 9 NP900X4C Premium Ultrabook',
        N'samsung-series-9-np900x4c-premium-ultrabook-1.png',
        CAST(51199.9500 as decimal(18, 4)),
        N'Samsung Series 9 NP900X4C-A06US 15-Inch Ultrabook (1.70 GHz Intel Core i5-3317U Processor, 8GB DDR3, 128GB SSD, Windows 8) Ash Black.')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (6, N'HP Spectre XT Pro UltraBook',
        N'hp-spectre-xt-pro-ultrabook-1.png',
        CAST(44199.9500 as decimal(18, 4)),
        N'HP Spectre XT Pro UltraBook / Intel Core i5-2467M / 13.3 / 4GB / 128GB / Windows 7 Professional / Laptop.')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (7, N'HP Envy 6-1180ca 15.6-Inch Sleekbook',
        N'hp-envy-6-1180ca-15.6-inch-sleekbook-1.png',
        CAST(61599.9500 as decimal(18, 4)),
        N'HP ENVY 6-1202ea Ultrabook Beats Audio, 3rd generation Intel® CoreTM i7-3517U processor, 8GB RAM, 500GB HDD, Microsoft Windows 8, AMD Radeon HD 8750M (2 GB DDR3 dedicated).')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (8, N'Lenovo Thinkpad X1 Carbon Laptop',
        N'lenovo-thinkpad-x1-carbon-laptop-1.png',
        CAST(54399.9500 as decimal(18, 4)),
        N'Lenovo Thinkpad X1 Carbon Touch Intel Core i7 14 Ultrabook.')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (9, N'Adobe Photoshop CS4',
        N'adobe-photoshop-cs4-1.png',
        CAST(1599.9500 as decimal(18, 4)),
        N'Easily find and view all your photos')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (10, N'Windows 8 Pro',
        N'windows-8-pro-1.png',
        CAST(4599.9500 as decimal(18, 4)),
        N'Windows 8 is a Microsoft operating system that was released in 2012 as part of the company''s Windows NT OS family.')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (11, N'Nikon D5500 DSLR',
        N'nikon-d5500-dslr-1.png',
        CAST(14799.0000 as decimal(18, 4)),
        N'Slim, lightweight Nikon D5500 packs a vari-angle touchscreen')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (12, N'Nikon D5500 DSLR - Black',
        N'nikon-d5500-dslr-black-1.png',
        CAST(14599.0000 as decimal(18, 4)),
        N'Slim, lightweight Nikon D5500 packs a vari-angle touchscreen')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (13, N'Nikon D5500 DSLR - Red',
        N'nikon-d5500-dslr-red-1.png',
        CAST(14999.0000 as decimal(18, 4)),
        N'Slim, lightweight Nikon D5500 packs a vari-angle touchscreen')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (14, N'Leica T Mirrorless Digital Camera',
        N'leica-t-mirrorless-digital-camera-1.png',
        CAST(23500.0000 as decimal(18, 4)),
        N'Leica T (Typ 701) Silver')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (15, N'Apple iCam',
        N'apple-icam-1.png',
        CAST(7999.0000 as decimal(18, 4)),
        N'Photography becomes smart')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (16, N'Universal 7-8 Inch Tablet Cover',
        N'universal-7-8-inch-tablet-cover-1.png',
        CAST(599.0000 as decimal(18, 4)),
        N'Universal protection for 7-inch & 8-inch tablets')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (17, N'Portable Sound Speakers',
        N'portable-sound-speakers-1.png',
        CAST(899.0000 as decimal(18, 4)),
        N'Universal portable sound speakers')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (18, N'Nike Floral Roshe Customized Running Shoes',
        N'nike-floral-roshe-customized-running-shoes-1.png',
        CAST(1349.5000 as decimal(18, 4)),
        N'When you ran across these shoes, you will immediately fell in love and needed a pair of these customized beauties.')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (19, N'adidas Consortium Campus 80s Running Shoes',
        N'adidas-consortium-campus-80s-running-shoes-1.png',
        CAST(2349.5000 as decimal(18, 4)),
        N'adidas Consortium Campus 80s Primeknit Light Maroon/Running Shoes')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (20, N'Nike SB Zoom Stefan Janoski "Medium Mint"',
        N'nike-sb-zoom-stefan-janoski-medium-mint-1.png',
        CAST(2149.5000 as decimal(18, 4)),
        N'Nike SB Zoom Stefan Janoski Dark Grey Medium Mint Teal ...')

insert into [Products] ([Id], [Name], [ImgUri], [Price], [Description])
values (21, N'Nike Tailwind Loose Short-Sleeve Running Shirt',
        N'nike-tailwind-loose-short-sleeve-running-shirt-1.png',
        CAST(849.9900 as decimal(18, 4)),
        N'Boost your adrenaline with the Nike® Women''s Tailwind Running Shirt. The lightweight, slouchy fit is great for layering, and moisture-wicking fabrics keep you feeling at your best.')