-- Name: event; Type: TABLE; Schema: public; Owner: ticketuser
--

CREATE TABLE event (
    id integer NOT NULL,
    event_name character varying(100),
    date_of_event date
);

--
-- Name: event_id_seq; Type: SEQUENCE; Schema: public; Owner: ticketuser
--

CREATE SEQUENCE event_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

--
-- Name: ticket; Type: TABLE; Schema: public; Owner: ticketuser
--

CREATE TABLE ticket (
    id integer NOT NULL,
    event_id integer,
    user_email character varying(100),
    is_scanned boolean,
    ticketnumber character varying(100) NOT NULL
);

CREATE SEQUENCE ticket_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


-- Name: event id; Type: DEFAULT; Schema: public; Owner: ticketuser
--

ALTER TABLE ONLY event ALTER COLUMN id SET DEFAULT nextval('public.event_id_seq'::regclass);


--
-- Name: ticket id; Type: DEFAULT; Schema: public; Owner: ticketuser
--

ALTER TABLE ONLY ticket ALTER COLUMN id SET DEFAULT nextval('public.ticket_id_seq'::regclass);

--
-- Data for Name: event; Type: TABLE DATA; Schema: public; Owner: ticketuser
--

COPY event (id, event_name, date_of_event) FROM stdin;
1	Circus	2024-02-10
2	Carnival	2024-02-20
3	Rodeo	2024-02-05
4	Monster Truck Derby	2024-02-15
\.


--
-- Data for Name: ticket; Type: TABLE DATA; Schema: public; Owner: ticketuser
--

COPY ticket (id, event_id, user_email, is_scanned, ticketnumber) FROM stdin;
51	3	brycegcoon@gmail.com	f	a3cd6ad1-7e18-47a6-9f4f-ac9334b1fa75
52	3	utaaronallen@gmail.com	f	6fc041fe-c8c4-474d-a3ca-eafdcddbda39
58	1	brycegcoon@gmail.com	t	800d3393-beac-4681-9669-b068b1476ebc
71	1	utaaronallen@gmail.com	t	ff991fa0-e21f-4364-90bd-a52e353d4a0d
73	1	brycegcoon@gmail.com	f	05303288-3ff9-4f07-8290-e977f67fe508
80	4	thisbetternotbearealemailaddress@gmail.com	f	7351e813-b982-4235-b0b6-af69c3e84021
82	3	brycegcoon@gmail.com	f	2480d4dc-5adb-477d-aa09-5635006078aa
86	1	brycegcoon@gmail.com	f	8506db95-b281-43ad-8365-d90dec38c531
30	2	brycegcoon@gmail.com	f	32b101fd-951d-4f5e-837f-a7e07b4943cb
87	3	utaaronallen@gmail.com	f	0b83dec5-7606-458b-b481-3abb9f4ec5a2
40	3	utaaronallen@gmail.com	f	43deade4-0c58-446d-810c-72ff78a6ed34
37	4	utaaronallen@gmail.com	f	dacdfb22-6216-4b5a-8b24-fb4e68b246f0
44	3	brycegcoon@gmail.com	f	0482e1b6-f074-438d-b90c-f06d1cbd7bd7
35	3	brycegcoon@gmail.com	f	68fff800-e965-4dba-b311-e399cb861e79
\.


--
-- Name: event_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ticketuser
--

SELECT pg_catalog.setval('event_id_seq', 4, true);


--
-- Name: ticket_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ticketuser
--

SELECT pg_catalog.setval('ticket_id_seq', 18, true);


--
-- Name: event event_pkey; Type: CONSTRAINT; Schema: public; Owner: ticketuser
--

ALTER TABLE ONLY event
    ADD CONSTRAINT event_pkey PRIMARY KEY (id);


--
-- Name: ticket ticket_pkey; Type: CONSTRAINT; Schema: public; Owner: ticketuser
--

ALTER TABLE ONLY ticket
    ADD CONSTRAINT ticket_pkey PRIMARY KEY (id);


--
-- Name: ticket ticket_event_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: ticketuser
--

ALTER TABLE ONLY ticket
    ADD CONSTRAINT ticket_event_id_fkey FOREIGN KEY (event_id) REFERENCES event(id);


--
-- PostgreSQL database dump complete
--

