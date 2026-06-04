--
-- PostgreSQL database dump
--

\restrict 05fyiq0cbIrmkRmXo5M7TbvsmWbuUqRFxvOdwcEbEn2gqm30GdHwyNLzUrMPink

-- Dumped from database version 15.17 (Homebrew)
-- Dumped by pg_dump version 15.17 (Homebrew)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Data for Name: AspNetRoles; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."AspNetRoles" VALUES ('7ae4e50f-451b-444d-867e-b639408dc4c5', 'Admin', 'ADMIN', NULL);
INSERT INTO public."AspNetRoles" VALUES ('78db88ba-83af-42b6-95a2-cca6493b6186', 'User', 'USER', NULL);


--
-- Data for Name: AspNetRoleClaims; Type: TABLE DATA; Schema: public; Owner: mitza
--



--
-- Data for Name: AspNetUsers; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."AspNetUsers" VALUES ('119c8c46-5bf1-44a5-8d6d-2f1a9930f1a0', 'mihaip781@gmail.com', 'MIHAIP781@GMAIL.COM', 'mihaip781@gmail.com', 'MIHAIP781@GMAIL.COM', false, 'AQAAAAIAAYagAAAAEHz9/CtBXdVxT2Mv2WHzoCBmqIB+FsR1JAN6ZVhTnboL80BWwfkYNUNEmoxXFRJkwA==', 'XTKXC2SM77QA5ZIFRTRWQICLWA7LZOHS', 'd6b60923-a10a-46f4-89ef-bb46aa61af0b', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('c58376d7-3f9d-42cf-8dfa-2b31c1bcbcf7', 'admin@realmadrid.com', 'ADMIN@REALMADRID.COM', 'admin@realmadrid.com', 'ADMIN@REALMADRID.COM', true, 'AQAAAAIAAYagAAAAEGoZL1dAiatNrhDKzTjz+RJu/SX/GZOqxOSlR1e6weOV2/pmNlYhUvidVENvIGowdg==', 'ZU7HUE5JCWZEVRJRIJJCXWL2SDAVKK77', '1d631b97-dbeb-4bbb-8ca5-8ce8180a3611', NULL, false, false, NULL, true, 0);
INSERT INTO public."AspNetUsers" VALUES ('41469e61-d408-4d4c-8fde-72d66f09da9d', 'teste@test.com', 'TESTE@TEST.COM', 'teste@test.com', 'TESTE@TEST.COM', false, 'AQAAAAIAAYagAAAAEHQiSXpYmI70LU8rXiGUXADuxyeoqHgkkSvWbuo1de9Rb53Znsx8mUunNjpe9NdZzA==', 'TAN5YYCU74FQJO7TY3WL2526NN3L5XRZ', '8899d4a3-a38a-4841-815d-3d7cabe0b936', NULL, false, false, NULL, true, 0);


--
-- Data for Name: AspNetUserClaims; Type: TABLE DATA; Schema: public; Owner: mitza
--



--
-- Data for Name: AspNetUserLogins; Type: TABLE DATA; Schema: public; Owner: mitza
--



--
-- Data for Name: AspNetUserRoles; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."AspNetUserRoles" VALUES ('c58376d7-3f9d-42cf-8dfa-2b31c1bcbcf7', '7ae4e50f-451b-444d-867e-b639408dc4c5');


--
-- Data for Name: AspNetUserTokens; Type: TABLE DATA; Schema: public; Owner: mitza
--



--
-- Data for Name: Teams; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."Teams" VALUES (1, 'First Team', 'https://publish.realmadrid.com/content/dam/portals/realmadrid-com/es-es/news/generic/2026/01/04/(fut)-rm-betis-j18-liga/fotos/ND_LIGA_18_RM_BETIS_ONCE_AV18172.jpg');
INSERT INTO public."Teams" VALUES (2, 'Women''s Team', 'https://a.espncdn.com/photo/2022/1030/r1083472_1296x729_16-9.jpg');


--
-- Data for Name: Players; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."Players" VALUES (1, 'Thibaut Courtois', 'Goalkeeper', 1, 0, 0, 1, 'https://publish.realmadrid.com/content/dam/portals/realmadrid-com/es-es/sports/football/3kq9cckrnlogidldtdie2fkbl/players/thibaut-courtois/assets/COURTOIS_550x650_SinParche.png');
INSERT INTO public."Players" VALUES (7, 'Athenea del Castillo', 'Forward', 7, 5, 1, 2, 'https://publish.realmadrid.com/content/dam/portals/realmadrid-com/es-es/sports/football/vaikl8nl7hdr4y9jj4jyb9ey/players/athenea-del-castillo-beivide/ATHENEA_AV38307_550x650.png');
INSERT INTO public."Players" VALUES (6, 'Jude Bellingham', 'Midfielder', 5, 2, 0, 1, 'https://assets.realmadrid.com/is/image/realmadrid/BELLINGHAM_550x650_SinParche?$Mobile$&fit=wrap&wid=420');
INSERT INTO public."Players" VALUES (2, 'Vincius Jr.', 'Forward', 7, 5, 0, 1, 'https://assets.laliga.com/squad/2025/t186/p246333/2048x2225/p246333_t186_2025_1_001_000.png');


--
-- Data for Name: FavoritePlayers; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."FavoritePlayers" VALUES (2, '41469e61-d408-4d4c-8fde-72d66f09da9d', 1, '2026-05-26 12:12:53.820308');
INSERT INTO public."FavoritePlayers" VALUES (3, '119c8c46-5bf1-44a5-8d6d-2f1a9930f1a0', 2, '2026-05-26 12:32:12.853331');


--
-- Data for Name: Matches; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."Matches" VALUES (3, 'Atletico Madrid', '2026-06-28 19:45:00', 'Santiago Bernabéu', 'https://images.seeklogo.com/logo-png/29/1/atletico-madrid-new-logo-png_seeklogo-297708.png', 'https://upload.wikimedia.org/wikipedia/sco/thumb/5/56/Real_Madrid_CF.svg/960px-Real_Madrid_CF.svg.png', 'La Liga Female', 2, 0, 0, false, 'Home');
INSERT INTO public."Matches" VALUES (1, 'Barcelona', '2026-06-20 18:45:00', 'Santiago Bernabéu', 'https://upload.wikimedia.org/wikipedia/en/thumb/4/47/FC_Barcelona_%28crest%29.svg/1280px-FC_Barcelona_%28crest%29.svg.png', 'https://upload.wikimedia.org/wikipedia/sco/thumb/5/56/Real_Madrid_CF.svg/960px-Real_Madrid_CF.svg.png', 'La Liga', 1, 1, 0, false, 'Home');
INSERT INTO public."Matches" VALUES (4, 'Bayern Munchen', '2026-05-08 15:10:00', 'Santiago Bernabéu', 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/1b/FC_Bayern_München_logo_%282017%29.svg/1280px-FC_Bayern_München_logo_%282017%29.svg.png?utm_source=commons.wikimedia.org&utm_campaign=index&utm_content=thumbnail', 'https://upload.wikimedia.org/wikipedia/sco/thumb/5/56/Real_Madrid_CF.svg/960px-Real_Madrid_CF.svg.png', 'Champions League', 1, 0, 1, true, 'Home');


--
-- Data for Name: MatchComments; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."MatchComments" VALUES (1, 3, '119c8c46-5bf1-44a5-8d6d-2f1a9930f1a0', 'Foarte bun meci, abia aștept sa îl vad.', '2026-05-26 12:08:00.757225');


--
-- Data for Name: PlayerMatches; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."PlayerMatches" VALUES (1, 1, 4, 90, 7);
INSERT INTO public."PlayerMatches" VALUES (2, 2, 4, 90, 9);
INSERT INTO public."PlayerMatches" VALUES (3, 6, 4, 90, 8.5);


--
-- Data for Name: Sponsors; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."Sponsors" VALUES (1, 'Adidas', 'Global Partner', 'https://upload.wikimedia.org/wikipedia/commons/2/24/Adidas_logo.png');
INSERT INTO public."Sponsors" VALUES (2, 'Emirates', 'Main Sponsor', 'https://logos-world.net/wp-content/uploads/2020/03/Emirates-Logo-1999.jpg');


--
-- Data for Name: Staff; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."Staff" VALUES (1, 'Jose Mourinho', 'Manager', 1, 'https://upload.wikimedia.org/wikipedia/commons/6/61/Mourinho_Madrid.jpg');
INSERT INTO public."Staff" VALUES (2, 'Florentino Perez', 'Leader', 1, 'https://assets.realmadrid.com/is/image/realmadrid/1330512967922?$Mobile$&fit=wrap');


--
-- Data for Name: TeamSponsors; Type: TABLE DATA; Schema: public; Owner: mitza
--



--
-- Data for Name: WatchlistMatches; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."WatchlistMatches" VALUES (2, '41469e61-d408-4d4c-8fde-72d66f09da9d', 3, '2026-05-26 12:12:46.480379');


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: mitza
--

INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523125308_InitialRealMadridDb', '8.0.4');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523130432_AddMatchAndPlayerMatch', '8.0.4');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523132256_CompleteSixEntitiesSchema', '8.0.4');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523140727_AddIdentityTables', '8.0.11');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523150342_FixPlayerImageUrlColumn', '8.0.11');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523151941_Images', '8.0.11');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523154443_Matches', '8.0.11');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523155514_AddScoresToMatchesTable', '8.0.11');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523160505_AddVenueToMatchesTable', '8.0.11');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523160915_UpdateSponsorsTableFinal', '8.0.11');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523161513_AddStatsToPlayersTableFinal', '8.0.11');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260523162812_AddStatsToPlayers', '8.0.11');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20260526120640_NewMigration', '8.0.11');


--
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."AspNetRoleClaims_Id_seq"', 1, false);


--
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."AspNetUserClaims_Id_seq"', 1, false);


--
-- Name: FavoritePlayers_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."FavoritePlayers_Id_seq"', 3, true);


--
-- Name: MatchComments_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."MatchComments_Id_seq"', 1, true);


--
-- Name: Matches_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."Matches_Id_seq"', 4, true);


--
-- Name: PlayerMatches_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."PlayerMatches_Id_seq"', 3, true);


--
-- Name: Players_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."Players_Id_seq"', 9, true);


--
-- Name: Sponsors_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."Sponsors_Id_seq"', 2, true);


--
-- Name: Staff_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."Staff_Id_seq"', 2, true);


--
-- Name: TeamSponsors_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."TeamSponsors_Id_seq"', 1, false);


--
-- Name: Teams_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."Teams_Id_seq"', 2, true);


--
-- Name: WatchlistMatches_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: mitza
--

SELECT pg_catalog.setval('public."WatchlistMatches_Id_seq"', 2, true);


--
-- PostgreSQL database dump complete
--

\unrestrict 05fyiq0cbIrmkRmXo5M7TbvsmWbuUqRFxvOdwcEbEn2gqm30GdHwyNLzUrMPink

