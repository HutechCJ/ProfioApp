create table if not exists databasechangeloglock
(
    id          integer not null
        constraint databasechangeloglock_pkey
            primary key,
    locked      boolean not null,
    lockgranted timestamp,
    lockedby    varchar(255)
);

alter table databasechangeloglock
    owner to admin;

create table if not exists databasechangelog
(
    id            varchar(255) not null,
    author        varchar(255) not null,
    filename      varchar(255) not null,
    dateexecuted  timestamp    not null,
    orderexecuted integer      not null,
    exectype      varchar(10)  not null,
    md5sum        varchar(35),
    description   varchar(255),
    comments      varchar(255),
    tag           varchar(255),
    liquibase     varchar(20),
    contexts      varchar(255),
    labels        varchar(255),
    deployment_id varchar(10)
);

alter table databasechangelog
    owner to admin;

create table if not exists client
(
    id                           varchar(36)           not null
        constraint constraint_7
            primary key,
    enabled                      boolean default false not null,
    full_scope_allowed           boolean default false not null,
    client_id                    varchar(255),
    not_before                   integer,
    public_client                boolean default false not null,
    secret                       varchar(255),
    base_url                     varchar(255),
    bearer_only                  boolean default false not null,
    management_url               varchar(255),
    surrogate_auth_required      boolean default false not null,
    realm_id                     varchar(36),
    protocol                     varchar(255),
    node_rereg_timeout           integer default 0,
    frontchannel_logout          boolean default false not null,
    consent_required             boolean default false not null,
    name                         varchar(255),
    service_accounts_enabled     boolean default false not null,
    client_authenticator_type    varchar(255),
    root_url                     varchar(255),
    description                  varchar(255),
    registration_token           varchar(255),
    standard_flow_enabled        boolean default true  not null,
    implicit_flow_enabled        boolean default false not null,
    direct_access_grants_enabled boolean default false not null,
    always_display_in_console    boolean default false not null,
    constraint uk_b71cjlbenv945rb6gcon438at
        unique (realm_id, client_id)
);

alter table client
    owner to admin;

create index if not exists idx_client_id
    on client (client_id);

create table if not exists event_entity
(
    id           varchar(36) not null
        constraint constraint_4
            primary key,
    client_id    varchar(255),
    details_json varchar(2550),
    error        varchar(255),
    ip_address   varchar(255),
    realm_id     varchar(255),
    session_id   varchar(255),
    event_time   bigint,
    type         varchar(255),
    user_id      varchar(255)
);

alter table event_entity
    owner to admin;

create index if not exists idx_event_time
    on event_entity (realm_id, event_time);

create table if not exists realm
(
    id                           varchar(36)               not null
        constraint constraint_4a
            primary key,
    access_code_lifespan         integer,
    user_action_lifespan         integer,
    access_token_lifespan        integer,
    account_theme                varchar(255),
    admin_theme                  varchar(255),
    email_theme                  varchar(255),
    enabled                      boolean     default false not null,
    events_enabled               boolean     default false not null,
    events_expiration            bigint,
    login_theme                  varchar(255),
    name                         varchar(255)
        constraint uk_orvsdmla56612eaefiq6wl5oi
            unique,
    not_before                   integer,
    password_policy              varchar(2550),
    registration_allowed         boolean     default false not null,
    remember_me                  boolean     default false not null,
    reset_password_allowed       boolean     default false not null,
    social                       boolean     default false not null,
    ssl_required                 varchar(255),
    sso_idle_timeout             integer,
    sso_max_lifespan             integer,
    update_profile_on_soc_login  boolean     default false not null,
    verify_email                 boolean     default false not null,
    master_admin_client          varchar(36),
    login_lifespan               integer,
    internationalization_enabled boolean     default false not null,
    default_locale               varchar(255),
    reg_email_as_username        boolean     default false not null,
    admin_events_enabled         boolean     default false not null,
    admin_events_details_enabled boolean     default false not null,
    edit_username_allowed        boolean     default false not null,
    otp_policy_counter           integer     default 0,
    otp_policy_window            integer     default 1,
    otp_policy_period            integer     default 30,
    otp_policy_digits            integer     default 6,
    otp_policy_alg               varchar(36) default 'HmacSHA1'::character varying,
    otp_policy_type              varchar(36) default 'totp'::character varying,
    browser_flow                 varchar(36),
    registration_flow            varchar(36),
    direct_grant_flow            varchar(36),
    reset_credentials_flow       varchar(36),
    client_auth_flow             varchar(36),
    offline_session_idle_timeout integer     default 0,
    revoke_refresh_token         boolean     default false not null,
    access_token_life_implicit   integer     default 0,
    login_with_email_allowed     boolean     default true  not null,
    duplicate_emails_allowed     boolean     default false not null,
    docker_auth_flow             varchar(36),
    refresh_token_max_reuse      integer     default 0,
    allow_user_managed_access    boolean     default false not null,
    sso_max_lifespan_remember_me integer     default 0     not null,
    sso_idle_timeout_remember_me integer     default 0     not null,
    default_role                 varchar(255)
);

alter table realm
    owner to admin;

create table if not exists keycloak_role
(
    id                      varchar(36)           not null
        constraint constraint_a
            primary key,
    client_realm_constraint varchar(255),
    client_role             boolean default false not null,
    description             varchar(255),
    name                    varchar(255),
    realm_id                varchar(255),
    client                  varchar(36),
    realm                   varchar(36)
        constraint fk_6vyqfe4cn4wlq8r6kt5vdsj5c
            references realm,
    constraint "UK_J3RWUVD56ONTGSUHOGM184WW2-2"
        unique (name, client_realm_constraint)
);

alter table keycloak_role
    owner to admin;

create table if not exists composite_role
(
    composite  varchar(36) not null
        constraint fk_a63wvekftu8jo1pnj81e7mce2
            references keycloak_role,
    child_role varchar(36) not null
        constraint fk_gr7thllb9lu8q4vqa4524jjy8
            references keycloak_role,
    constraint constraint_composite_role
        primary key (composite, child_role)
);

alter table composite_role
    owner to admin;

create index if not exists idx_composite
    on composite_role (composite);

create index if not exists idx_composite_child
    on composite_role (child_role);

create index if not exists idx_keycloak_role_client
    on keycloak_role (client);

create index if not exists idx_keycloak_role_realm
    on keycloak_role (realm);

create index if not exists idx_realm_master_adm_cli
    on realm (master_admin_client);

create table if not exists realm_attribute
(
    name     varchar(255) not null,
    realm_id varchar(36)  not null
        constraint fk_8shxd6l3e9atqukacxgpffptw
            references realm,
    value    text,
    constraint constraint_9
        primary key (name, realm_id)
);

alter table realm_attribute
    owner to admin;

create index if not exists idx_realm_attr_realm
    on realm_attribute (realm_id);

create table if not exists realm_events_listeners
(
    realm_id varchar(36)  not null
        constraint fk_h846o4h0w8epx5nxev9f5y69j
            references realm,
    value    varchar(255) not null,
    constraint constr_realm_events_listeners
        primary key (realm_id, value)
);

alter table realm_events_listeners
    owner to admin;

create index if not exists idx_realm_evt_list_realm
    on realm_events_listeners (realm_id);

create table if not exists realm_required_credential
(
    type       varchar(255)          not null,
    form_label varchar(255),
    input      boolean default false not null,
    secret     boolean default false not null,
    realm_id   varchar(36)           not null
        constraint fk_5hg65lybevavkqfki3kponh9v
            references realm,
    constraint constraint_92
        primary key (realm_id, type)
);

alter table realm_required_credential
    owner to admin;

create table if not exists realm_smtp_config
(
    realm_id varchar(36)  not null
        constraint fk_70ej8xdxgxd0b9hh6180irr0o
            references realm,
    value    varchar(255),
    name     varchar(255) not null,
    constraint constraint_e
        primary key (realm_id, name)
);

alter table realm_smtp_config
    owner to admin;

create table if not exists redirect_uris
(
    client_id varchar(36)  not null
        constraint fk_1burs8pb4ouj97h5wuppahv9f
            references client,
    value     varchar(255) not null,
    constraint constraint_redirect_uris
        primary key (client_id, value)
);

alter table redirect_uris
    owner to admin;

create index if not exists idx_redir_uri_client
    on redirect_uris (client_id);

create table if not exists scope_mapping
(
    client_id varchar(36) not null
        constraint fk_ouse064plmlr732lxjcn1q5f1
            references client,
    role_id   varchar(36) not null,
    constraint constraint_81
        primary key (client_id, role_id)
);

alter table scope_mapping
    owner to admin;

create index if not exists idx_scope_mapping_role
    on scope_mapping (role_id);

create table if not exists username_login_failure
(
    realm_id                varchar(36)  not null,
    username                varchar(255) not null,
    failed_login_not_before integer,
    last_failure            bigint,
    last_ip_failure         varchar(255),
    num_failures            integer,
    constraint "CONSTRAINT_17-2"
        primary key (realm_id, username)
);

alter table username_login_failure
    owner to admin;

create table if not exists user_entity
(
    id                          varchar(36)           not null
        constraint constraint_fb
            primary key,
    email                       varchar(255),
    email_constraint            varchar(255),
    email_verified              boolean default false not null,
    enabled                     boolean default false not null,
    federation_link             varchar(255),
    first_name                  varchar(255),
    last_name                   varchar(255),
    realm_id                    varchar(255),
    username                    varchar(255),
    created_timestamp           bigint,
    service_account_client_link varchar(255),
    not_before                  integer default 0     not null,
    constraint uk_dykn684sl8up1crfei6eckhd7
        unique (realm_id, email_constraint),
    constraint uk_ru8tt6t700s9v50bu18ws5ha6
        unique (realm_id, username)
);

alter table user_entity
    owner to admin;

create table if not exists credential
(
    id              varchar(36) not null
        constraint constraint_f
            primary key,
    salt            bytea,
    type            varchar(255),
    user_id         varchar(36)
        constraint fk_pfyr0glasqyl0dei3kl69r6v0
            references user_entity,
    created_date    bigint,
    user_label      varchar(255),
    secret_data     text,
    credential_data text,
    priority        integer
);

alter table credential
    owner to admin;

create index if not exists idx_user_credential
    on credential (user_id);

create table if not exists user_attribute
(
    name    varchar(255)                                                         not null,
    value   varchar(255),
    user_id varchar(36)                                                          not null
        constraint fk_5hrm2vlf9ql5fu043kqepovbr
            references user_entity,
    id      varchar(36) default 'sybase-needs-something-here'::character varying not null
        constraint constraint_user_attribute_pk
            primary key
);

alter table user_attribute
    owner to admin;

create index if not exists idx_user_attribute
    on user_attribute (user_id);

create index if not exists idx_user_attribute_name
    on user_attribute (name, value);

create index if not exists idx_user_email
    on user_entity (email);

create index if not exists idx_user_service_account
    on user_entity (realm_id, service_account_client_link);

create table if not exists user_federation_provider
(
    id                  varchar(36) not null
        constraint constraint_5c
            primary key,
    changed_sync_period integer,
    display_name        varchar(255),
    full_sync_period    integer,
    last_sync           integer,
    priority            integer,
    provider_name       varchar(255),
    realm_id            varchar(36)
        constraint fk_1fj32f6ptolw2qy60cd8n01e8
            references realm
);

alter table user_federation_provider
    owner to admin;

create table if not exists user_federation_config
(
    user_federation_provider_id varchar(36)  not null
        constraint fk_t13hpu1j94r2ebpekr39x5eu5
            references user_federation_provider,
    value                       varchar(255),
    name                        varchar(255) not null,
    constraint constraint_f9
        primary key (user_federation_provider_id, name)
);

alter table user_federation_config
    owner to admin;

create index if not exists idx_usr_fed_prv_realm
    on user_federation_provider (realm_id);

create table if not exists user_required_action
(
    user_id         varchar(36)                                 not null
        constraint fk_6qj3w1jw9cvafhe19bwsiuvmd
            references user_entity,
    required_action varchar(255) default ' '::character varying not null,
    constraint constraint_required_action
        primary key (required_action, user_id)
);

alter table user_required_action
    owner to admin;

create index if not exists idx_user_reqactions
    on user_required_action (user_id);

create table if not exists user_role_mapping
(
    role_id varchar(255) not null,
    user_id varchar(36)  not null
        constraint fk_c4fqv34p1mbylloxang7b1q3l
            references user_entity,
    constraint constraint_c
        primary key (role_id, user_id)
);

alter table user_role_mapping
    owner to admin;

create index if not exists idx_user_role_mapping
    on user_role_mapping (user_id);

create table if not exists user_session
(
    id                   varchar(36)           not null
        constraint constraint_57
            primary key,
    auth_method          varchar(255),
    ip_address           varchar(255),
    last_session_refresh integer,
    login_username       varchar(255),
    realm_id             varchar(255),
    remember_me          boolean default false not null,
    started              integer,
    user_id              varchar(255),
    user_session_state   integer,
    broker_session_id    varchar(255),
    broker_user_id       varchar(255)
);

alter table user_session
    owner to admin;

create table if not exists client_session
(
    id             varchar(36) not null
        constraint constraint_8
            primary key,
    client_id      varchar(36),
    redirect_uri   varchar(255),
    state          varchar(255),
    timestamp      integer,
    session_id     varchar(36)
        constraint fk_b4ao2vcvat6ukau74wbwtfqo1
            references user_session,
    auth_method    varchar(255),
    realm_id       varchar(255),
    auth_user_id   varchar(36),
    current_action varchar(36)
);

alter table client_session
    owner to admin;

create index if not exists idx_client_session_session
    on client_session (session_id);

create table if not exists client_session_role
(
    role_id        varchar(255) not null,
    client_session varchar(36)  not null
        constraint fk_11b7sgqw18i532811v7o2dv76
            references client_session,
    constraint constraint_5
        primary key (client_session, role_id)
);

alter table client_session_role
    owner to admin;

create table if not exists web_origins
(
    client_id varchar(36)  not null
        constraint fk_lojpho213xcx4wnkog82ssrfy
            references client,
    value     varchar(255) not null,
    constraint constraint_web_origins
        primary key (client_id, value)
);

alter table web_origins
    owner to admin;

create index if not exists idx_web_orig_client
    on web_origins (client_id);

create table if not exists client_attributes
(
    client_id varchar(36)  not null
        constraint fk3c47c64beacca966
            references client,
    name      varchar(255) not null,
    value     text,
    constraint constraint_3c
        primary key (client_id, name)
);

alter table client_attributes
    owner to admin;

create table if not exists client_session_note
(
    name           varchar(255) not null,
    value          varchar(255),
    client_session varchar(36)  not null
        constraint fk5edfb00ff51c2736
            references client_session,
    constraint constraint_5e
        primary key (client_session, name)
);

alter table client_session_note
    owner to admin;

create table if not exists client_node_registrations
(
    client_id varchar(36)  not null
        constraint fk4129723ba992f594
            references client,
    value     integer,
    name      varchar(255) not null,
    constraint constraint_84
        primary key (client_id, name)
);

alter table client_node_registrations
    owner to admin;

create table if not exists federated_identity
(
    identity_provider  varchar(255) not null,
    realm_id           varchar(36),
    federated_user_id  varchar(255),
    federated_username varchar(255),
    token              text,
    user_id            varchar(36)  not null
        constraint fk404288b92ef007a6
            references user_entity,
    constraint constraint_40
        primary key (identity_provider, user_id)
);

alter table federated_identity
    owner to admin;

create index if not exists idx_fedidentity_user
    on federated_identity (user_id);

create index if not exists idx_fedidentity_feduser
    on federated_identity (federated_user_id);

create table if not exists identity_provider
(
    internal_id                varchar(36)           not null
        constraint constraint_2b
            primary key,
    enabled                    boolean default false not null,
    provider_alias             varchar(255),
    provider_id                varchar(255),
    store_token                boolean default false not null,
    authenticate_by_default    boolean default false not null,
    realm_id                   varchar(36)
        constraint fk2b4ebc52ae5c3b34
            references realm,
    add_token_role             boolean default true  not null,
    trust_email                boolean default false not null,
    first_broker_login_flow_id varchar(36),
    post_broker_login_flow_id  varchar(36),
    provider_display_name      varchar(255),
    link_only                  boolean default false not null,
    constraint uk_2daelwnibji49avxsrtuf6xj33
        unique (provider_alias, realm_id)
);

alter table identity_provider
    owner to admin;

create index if not exists idx_ident_prov_realm
    on identity_provider (realm_id);

create table if not exists identity_provider_config
(
    identity_provider_id varchar(36)  not null
        constraint fkdc4897cf864c4e43
            references identity_provider,
    value                text,
    name                 varchar(255) not null,
    constraint constraint_d
        primary key (identity_provider_id, name)
);

alter table identity_provider_config
    owner to admin;

create table if not exists realm_supported_locales
(
    realm_id varchar(36)  not null
        constraint fk_supported_locales_realm
            references realm,
    value    varchar(255) not null,
    constraint constr_realm_supported_locales
        primary key (realm_id, value)
);

alter table realm_supported_locales
    owner to admin;

create index if not exists idx_realm_supp_local_realm
    on realm_supported_locales (realm_id);

create table if not exists user_session_note
(
    user_session varchar(36)  not null
        constraint fk5edfb00ff51d3472
            references user_session,
    name         varchar(255) not null,
    value        varchar(2048),
    constraint constraint_usn_pk
        primary key (user_session, name)
);

alter table user_session_note
    owner to admin;

create table if not exists realm_enabled_event_types
(
    realm_id varchar(36)  not null
        constraint fk_h846o4h0w8epx5nwedrf5y69j
            references realm,
    value    varchar(255) not null,
    constraint constr_realm_enabl_event_types
        primary key (realm_id, value)
);

alter table realm_enabled_event_types
    owner to admin;

create index if not exists idx_realm_evt_types_realm
    on realm_enabled_event_types (realm_id);

create table if not exists migration_model
(
    id          varchar(36)      not null
        constraint constraint_migmod
            primary key,
    version     varchar(36),
    update_time bigint default 0 not null
);

alter table migration_model
    owner to admin;

create index if not exists idx_update_time
    on migration_model (update_time);

create table if not exists identity_provider_mapper
(
    id              varchar(36)  not null
        constraint constraint_idpm
            primary key,
    name            varchar(255) not null,
    idp_alias       varchar(255) not null,
    idp_mapper_name varchar(255) not null,
    realm_id        varchar(36)  not null
        constraint fk_idpm_realm
            references realm
);

alter table identity_provider_mapper
    owner to admin;

create index if not exists idx_id_prov_mapp_realm
    on identity_provider_mapper (realm_id);

create table if not exists idp_mapper_config
(
    idp_mapper_id varchar(36)  not null
        constraint fk_idpmconfig
            references identity_provider_mapper,
    value         text,
    name          varchar(255) not null,
    constraint constraint_idpmconfig
        primary key (idp_mapper_id, name)
);

alter table idp_mapper_config
    owner to admin;

create table if not exists user_consent
(
    id                      varchar(36) not null
        constraint constraint_grntcsnt_pm
            primary key,
    client_id               varchar(255),
    user_id                 varchar(36) not null
        constraint fk_grntcsnt_user
            references user_entity,
    created_date            bigint,
    last_updated_date       bigint,
    client_storage_provider varchar(36),
    external_client_id      varchar(255),
    constraint uk_jkuwuvd56ontgsuhogm8uewrt
        unique (client_id, client_storage_provider, external_client_id, user_id)
);

alter table user_consent
    owner to admin;

create index if not exists idx_user_consent
    on user_consent (user_id);

create table if not exists client_session_prot_mapper
(
    protocol_mapper_id varchar(36) not null,
    client_session     varchar(36) not null
        constraint fk_33a8sgqw18i532811v7o2dk89
            references client_session,
    constraint constraint_cs_pmp_pk
        primary key (client_session, protocol_mapper_id)
);

alter table client_session_prot_mapper
    owner to admin;

create table if not exists admin_event_entity
(
    id               varchar(36) not null
        constraint constraint_admin_event_entity
            primary key,
    admin_event_time bigint,
    realm_id         varchar(255),
    operation_type   varchar(255),
    auth_realm_id    varchar(255),
    auth_client_id   varchar(255),
    auth_user_id     varchar(255),
    ip_address       varchar(255),
    resource_path    varchar(2550),
    representation   text,
    error            varchar(255),
    resource_type    varchar(64)
);

alter table admin_event_entity
    owner to admin;

create index if not exists idx_admin_event_time
    on admin_event_entity (realm_id, admin_event_time);

create table if not exists authenticator_config
(
    id       varchar(36) not null
        constraint constraint_auth_pk
            primary key,
    alias    varchar(255),
    realm_id varchar(36)
        constraint fk_auth_realm
            references realm
);

alter table authenticator_config
    owner to admin;

create index if not exists idx_auth_config_realm
    on authenticator_config (realm_id);

create table if not exists authentication_flow
(
    id          varchar(36)                                         not null
        constraint constraint_auth_flow_pk
            primary key,
    alias       varchar(255),
    description varchar(255),
    realm_id    varchar(36)
        constraint fk_auth_flow_realm
            references realm,
    provider_id varchar(36) default 'basic-flow'::character varying not null,
    top_level   boolean     default false                           not null,
    built_in    boolean     default false                           not null
);

alter table authentication_flow
    owner to admin;

create index if not exists idx_auth_flow_realm
    on authentication_flow (realm_id);

create table if not exists authentication_execution
(
    id                 varchar(36)           not null
        constraint constraint_auth_exec_pk
            primary key,
    alias              varchar(255),
    authenticator      varchar(36),
    realm_id           varchar(36)
        constraint fk_auth_exec_realm
            references realm,
    flow_id            varchar(36)
        constraint fk_auth_exec_flow
            references authentication_flow,
    requirement        integer,
    priority           integer,
    authenticator_flow boolean default false not null,
    auth_flow_id       varchar(36),
    auth_config        varchar(36)
);

alter table authentication_execution
    owner to admin;

create index if not exists idx_auth_exec_realm_flow
    on authentication_execution (realm_id, flow_id);

create index if not exists idx_auth_exec_flow
    on authentication_execution (flow_id);

create table if not exists authenticator_config_entry
(
    authenticator_id varchar(36)  not null,
    value            text,
    name             varchar(255) not null,
    constraint constraint_auth_cfg_pk
        primary key (authenticator_id, name)
);

alter table authenticator_config_entry
    owner to admin;

create table if not exists user_federation_mapper
(
    id                     varchar(36)  not null
        constraint constraint_fedmapperpm
            primary key,
    name                   varchar(255) not null,
    federation_provider_id varchar(36)  not null
        constraint fk_fedmapperpm_fedprv
            references user_federation_provider,
    federation_mapper_type varchar(255) not null,
    realm_id               varchar(36)  not null
        constraint fk_fedmapperpm_realm
            references realm
);

alter table user_federation_mapper
    owner to admin;

create index if not exists idx_usr_fed_map_fed_prv
    on user_federation_mapper (federation_provider_id);

create index if not exists idx_usr_fed_map_realm
    on user_federation_mapper (realm_id);

create table if not exists user_federation_mapper_config
(
    user_federation_mapper_id varchar(36)  not null
        constraint fk_fedmapper_cfg
            references user_federation_mapper,
    value                     varchar(255),
    name                      varchar(255) not null,
    constraint constraint_fedmapper_cfg_pm
        primary key (user_federation_mapper_id, name)
);

alter table user_federation_mapper_config
    owner to admin;

create table if not exists client_session_auth_status
(
    authenticator  varchar(36) not null,
    status         integer,
    client_session varchar(36) not null
        constraint auth_status_constraint
            references client_session,
    constraint constraint_auth_status_pk
        primary key (client_session, authenticator)
);

alter table client_session_auth_status
    owner to admin;

create table if not exists client_user_session_note
(
    name           varchar(255) not null,
    value          varchar(2048),
    client_session varchar(36)  not null
        constraint fk_cl_usr_ses_note
            references client_session,
    constraint constr_cl_usr_ses_note
        primary key (client_session, name)
);

alter table client_user_session_note
    owner to admin;

create table if not exists required_action_provider
(
    id             varchar(36)           not null
        constraint constraint_req_act_prv_pk
            primary key,
    alias          varchar(255),
    name           varchar(255),
    realm_id       varchar(36)
        constraint fk_req_act_realm
            references realm,
    enabled        boolean default false not null,
    default_action boolean default false not null,
    provider_id    varchar(255),
    priority       integer
);

alter table required_action_provider
    owner to admin;

create index if not exists idx_req_act_prov_realm
    on required_action_provider (realm_id);

create table if not exists required_action_config
(
    required_action_id varchar(36)  not null,
    value              text,
    name               varchar(255) not null,
    constraint constraint_req_act_cfg_pk
        primary key (required_action_id, name)
);

alter table required_action_config
    owner to admin;

create table if not exists offline_user_session
(
    user_session_id      varchar(36)       not null,
    user_id              varchar(255)      not null,
    realm_id             varchar(36)       not null,
    created_on           integer           not null,
    offline_flag         varchar(4)        not null,
    data                 text,
    last_session_refresh integer default 0 not null,
    constraint constraint_offl_us_ses_pk2
        primary key (user_session_id, offline_flag)
);

alter table offline_user_session
    owner to admin;

create index if not exists idx_offline_uss_createdon
    on offline_user_session (created_on);

create index if not exists idx_offline_uss_preload
    on offline_user_session (offline_flag, created_on, user_session_id);

create index if not exists idx_offline_uss_by_user
    on offline_user_session (user_id, realm_id, offline_flag);

create index if not exists idx_offline_uss_by_usersess
    on offline_user_session (realm_id, offline_flag, user_session_id);

create table if not exists offline_client_session
(
    user_session_id         varchar(36)                                     not null,
    client_id               varchar(255)                                    not null,
    offline_flag            varchar(4)                                      not null,
    timestamp               integer,
    data                    text,
    client_storage_provider varchar(36)  default 'local'::character varying not null,
    external_client_id      varchar(255) default 'local'::character varying not null,
    constraint constraint_offl_cl_ses_pk3
        primary key (user_session_id, client_id, client_storage_provider, external_client_id, offline_flag)
);

alter table offline_client_session
    owner to admin;

create index if not exists idx_us_sess_id_on_cl_sess
    on offline_client_session (user_session_id);

create index if not exists idx_offline_css_preload
    on offline_client_session (client_id, offline_flag);

create table if not exists keycloak_group
(
    id           varchar(36) not null
        constraint constraint_group
            primary key,
    name         varchar(255),
    parent_group varchar(36) not null,
    realm_id     varchar(36),
    constraint sibling_names
        unique (realm_id, parent_group, name)
);

alter table keycloak_group
    owner to admin;

create table if not exists group_role_mapping
(
    role_id  varchar(36) not null,
    group_id varchar(36) not null
        constraint fk_group_role_group
            references keycloak_group,
    constraint constraint_group_role
        primary key (role_id, group_id)
);

alter table group_role_mapping
    owner to admin;

create index if not exists idx_group_role_mapp_group
    on group_role_mapping (group_id);

create table if not exists group_attribute
(
    id       varchar(36) default 'sybase-needs-something-here'::character varying not null
        constraint constraint_group_attribute_pk
            primary key,
    name     varchar(255)                                                         not null,
    value    varchar(255),
    group_id varchar(36)                                                          not null
        constraint fk_group_attribute_group
            references keycloak_group
);

alter table group_attribute
    owner to admin;

create index if not exists idx_group_attr_group
    on group_attribute (group_id);

create index if not exists idx_group_att_by_name_value
    on group_attribute (name, (value::character varying(250)));

create table if not exists user_group_membership
(
    group_id varchar(36) not null,
    user_id  varchar(36) not null
        constraint fk_user_group_user
            references user_entity,
    constraint constraint_user_group
        primary key (group_id, user_id)
);

alter table user_group_membership
    owner to admin;

create index if not exists idx_user_group_mapping
    on user_group_membership (user_id);

create table if not exists realm_default_groups
(
    realm_id varchar(36) not null
        constraint fk_def_groups_realm
            references realm,
    group_id varchar(36) not null
        constraint con_group_id_def_groups
            unique,
    constraint constr_realm_default_groups
        primary key (realm_id, group_id)
);

alter table realm_default_groups
    owner to admin;

create index if not exists idx_realm_def_grp_realm
    on realm_default_groups (realm_id);

create table if not exists client_scope
(
    id          varchar(36) not null
        constraint pk_cli_template
            primary key,
    name        varchar(255),
    realm_id    varchar(36),
    description varchar(255),
    protocol    varchar(255),
    constraint uk_cli_scope
        unique (realm_id, name)
);

alter table client_scope
    owner to admin;

create table if not exists protocol_mapper
(
    id                   varchar(36)  not null
        constraint constraint_pcm
            primary key,
    name                 varchar(255) not null,
    protocol             varchar(255) not null,
    protocol_mapper_name varchar(255) not null,
    client_id            varchar(36)
        constraint fk_pcm_realm
            references client,
    client_scope_id      varchar(36)
        constraint fk_cli_scope_mapper
            references client_scope
);

alter table protocol_mapper
    owner to admin;

create index if not exists idx_protocol_mapper_client
    on protocol_mapper (client_id);

create index if not exists idx_clscope_protmap
    on protocol_mapper (client_scope_id);

create table if not exists protocol_mapper_config
(
    protocol_mapper_id varchar(36)  not null
        constraint fk_pmconfig
            references protocol_mapper,
    value              text,
    name               varchar(255) not null,
    constraint constraint_pmconfig
        primary key (protocol_mapper_id, name)
);

alter table protocol_mapper_config
    owner to admin;

create index if not exists idx_realm_clscope
    on client_scope (realm_id);

create table if not exists client_scope_attributes
(
    scope_id varchar(36)  not null
        constraint fk_cl_scope_attr_scope
            references client_scope,
    value    varchar(2048),
    name     varchar(255) not null,
    constraint pk_cl_tmpl_attr
        primary key (scope_id, name)
);

alter table client_scope_attributes
    owner to admin;

create index if not exists idx_clscope_attrs
    on client_scope_attributes (scope_id);

create table if not exists client_scope_role_mapping
(
    scope_id varchar(36) not null
        constraint fk_cl_scope_rm_scope
            references client_scope,
    role_id  varchar(36) not null,
    constraint pk_template_scope
        primary key (scope_id, role_id)
);

alter table client_scope_role_mapping
    owner to admin;

create index if not exists idx_clscope_role
    on client_scope_role_mapping (scope_id);

create index if not exists idx_role_clscope
    on client_scope_role_mapping (role_id);

create table if not exists resource_server
(
    id                   varchar(36)            not null
        constraint pk_resource_server
            primary key,
    allow_rs_remote_mgmt boolean  default false not null,
    policy_enforce_mode  smallint               not null,
    decision_strategy    smallint default 1     not null
);

alter table resource_server
    owner to admin;

create table if not exists resource_server_resource
(
    id                   varchar(36)           not null
        constraint constraint_farsr
            primary key,
    name                 varchar(255)          not null,
    type                 varchar(255),
    icon_uri             varchar(255),
    owner                varchar(255)          not null,
    resource_server_id   varchar(36)           not null
        constraint fk_frsrho213xcx4wnkog82ssrfy
            references resource_server,
    owner_managed_access boolean default false not null,
    display_name         varchar(255),
    constraint uk_frsr6t700s9v50bu18ws5ha6
        unique (name, owner, resource_server_id)
);

alter table resource_server_resource
    owner to admin;

create index if not exists idx_res_srv_res_res_srv
    on resource_server_resource (resource_server_id);

create table if not exists resource_server_scope
(
    id                 varchar(36)  not null
        constraint constraint_farsrs
            primary key,
    name               varchar(255) not null,
    icon_uri           varchar(255),
    resource_server_id varchar(36)  not null
        constraint fk_frsrso213xcx4wnkog82ssrfy
            references resource_server,
    display_name       varchar(255),
    constraint uk_frsrst700s9v50bu18ws5ha6
        unique (name, resource_server_id)
);

alter table resource_server_scope
    owner to admin;

create index if not exists idx_res_srv_scope_res_srv
    on resource_server_scope (resource_server_id);

create table if not exists resource_server_policy
(
    id                 varchar(36)  not null
        constraint constraint_farsrp
            primary key,
    name               varchar(255) not null,
    description        varchar(255),
    type               varchar(255) not null,
    decision_strategy  smallint,
    logic              smallint,
    resource_server_id varchar(36)  not null
        constraint fk_frsrpo213xcx4wnkog82ssrfy
            references resource_server,
    owner              varchar(255),
    constraint uk_frsrpt700s9v50bu18ws5ha6
        unique (name, resource_server_id)
);

alter table resource_server_policy
    owner to admin;

create index if not exists idx_res_serv_pol_res_serv
    on resource_server_policy (resource_server_id);

create table if not exists policy_config
(
    policy_id varchar(36)  not null
        constraint fkdc34197cf864c4e43
            references resource_server_policy,
    name      varchar(255) not null,
    value     text,
    constraint constraint_dpc
        primary key (policy_id, name)
);

alter table policy_config
    owner to admin;

create table if not exists resource_scope
(
    resource_id varchar(36) not null
        constraint fk_frsrpos13xcx4wnkog82ssrfy
            references resource_server_resource,
    scope_id    varchar(36) not null
        constraint fk_frsrps213xcx4wnkog82ssrfy
            references resource_server_scope,
    constraint constraint_farsrsp
        primary key (resource_id, scope_id)
);

alter table resource_scope
    owner to admin;

create index if not exists idx_res_scope_scope
    on resource_scope (scope_id);

create table if not exists resource_policy
(
    resource_id varchar(36) not null
        constraint fk_frsrpos53xcx4wnkog82ssrfy
            references resource_server_resource,
    policy_id   varchar(36) not null
        constraint fk_frsrpp213xcx4wnkog82ssrfy
            references resource_server_policy,
    constraint constraint_farsrpp
        primary key (resource_id, policy_id)
);

alter table resource_policy
    owner to admin;

create index if not exists idx_res_policy_policy
    on resource_policy (policy_id);

create table if not exists scope_policy
(
    scope_id  varchar(36) not null
        constraint fk_frsrpass3xcx4wnkog82ssrfy
            references resource_server_scope,
    policy_id varchar(36) not null
        constraint fk_frsrasp13xcx4wnkog82ssrfy
            references resource_server_policy,
    constraint constraint_farsrsps
        primary key (scope_id, policy_id)
);

alter table scope_policy
    owner to admin;

create index if not exists idx_scope_policy_policy
    on scope_policy (policy_id);

create table if not exists associated_policy
(
    policy_id            varchar(36) not null
        constraint fk_frsrpas14xcx4wnkog82ssrfy
            references resource_server_policy,
    associated_policy_id varchar(36) not null
        constraint fk_frsr5s213xcx4wnkog82ssrfy
            references resource_server_policy,
    constraint constraint_farsrpap
        primary key (policy_id, associated_policy_id)
);

alter table associated_policy
    owner to admin;

create index if not exists idx_assoc_pol_assoc_pol_id
    on associated_policy (associated_policy_id);

create table if not exists broker_link
(
    identity_provider   varchar(255) not null,
    storage_provider_id varchar(255),
    realm_id            varchar(36)  not null,
    broker_user_id      varchar(255),
    broker_username     varchar(255),
    token               text,
    user_id             varchar(255) not null,
    constraint constr_broker_link_pk
        primary key (identity_provider, user_id)
);

alter table broker_link
    owner to admin;

create table if not exists fed_user_attribute
(
    id                  varchar(36)  not null
        constraint constr_fed_user_attr_pk
            primary key,
    name                varchar(255) not null,
    user_id             varchar(255) not null,
    realm_id            varchar(36)  not null,
    storage_provider_id varchar(36),
    value               varchar(2024)
);

alter table fed_user_attribute
    owner to admin;

create index if not exists idx_fu_attribute
    on fed_user_attribute (user_id, realm_id, name);

create table if not exists fed_user_consent
(
    id                      varchar(36)  not null
        constraint constr_fed_user_consent_pk
            primary key,
    client_id               varchar(255),
    user_id                 varchar(255) not null,
    realm_id                varchar(36)  not null,
    storage_provider_id     varchar(36),
    created_date            bigint,
    last_updated_date       bigint,
    client_storage_provider varchar(36),
    external_client_id      varchar(255)
);

alter table fed_user_consent
    owner to admin;

create index if not exists idx_fu_consent_ru
    on fed_user_consent (realm_id, user_id);

create index if not exists idx_fu_cnsnt_ext
    on fed_user_consent (user_id, client_storage_provider, external_client_id);

create index if not exists idx_fu_consent
    on fed_user_consent (user_id, client_id);

create table if not exists fed_user_credential
(
    id                  varchar(36)  not null
        constraint constr_fed_user_cred_pk
            primary key,
    salt                bytea,
    type                varchar(255),
    created_date        bigint,
    user_id             varchar(255) not null,
    realm_id            varchar(36)  not null,
    storage_provider_id varchar(36),
    user_label          varchar(255),
    secret_data         text,
    credential_data     text,
    priority            integer
);

alter table fed_user_credential
    owner to admin;

create index if not exists idx_fu_credential
    on fed_user_credential (user_id, type);

create index if not exists idx_fu_credential_ru
    on fed_user_credential (realm_id, user_id);

create table if not exists fed_user_group_membership
(
    group_id            varchar(36)  not null,
    user_id             varchar(255) not null,
    realm_id            varchar(36)  not null,
    storage_provider_id varchar(36),
    constraint constr_fed_user_group
        primary key (group_id, user_id)
);

alter table fed_user_group_membership
    owner to admin;

create index if not exists idx_fu_group_membership
    on fed_user_group_membership (user_id, group_id);

create index if not exists idx_fu_group_membership_ru
    on fed_user_group_membership (realm_id, user_id);

create table if not exists fed_user_required_action
(
    required_action     varchar(255) default ' '::character varying not null,
    user_id             varchar(255)                                not null,
    realm_id            varchar(36)                                 not null,
    storage_provider_id varchar(36),
    constraint constr_fed_required_action
        primary key (required_action, user_id)
);

alter table fed_user_required_action
    owner to admin;

create index if not exists idx_fu_required_action
    on fed_user_required_action (user_id, required_action);

create index if not exists idx_fu_required_action_ru
    on fed_user_required_action (realm_id, user_id);

create table if not exists fed_user_role_mapping
(
    role_id             varchar(36)  not null,
    user_id             varchar(255) not null,
    realm_id            varchar(36)  not null,
    storage_provider_id varchar(36),
    constraint constr_fed_user_role
        primary key (role_id, user_id)
);

alter table fed_user_role_mapping
    owner to admin;

create index if not exists idx_fu_role_mapping
    on fed_user_role_mapping (user_id, role_id);

create index if not exists idx_fu_role_mapping_ru
    on fed_user_role_mapping (realm_id, user_id);

create table if not exists component
(
    id            varchar(36) not null
        constraint constr_component_pk
            primary key,
    name          varchar(255),
    parent_id     varchar(36),
    provider_id   varchar(36),
    provider_type varchar(255),
    realm_id      varchar(36)
        constraint fk_component_realm
            references realm,
    sub_type      varchar(255)
);

alter table component
    owner to admin;

create table if not exists component_config
(
    id           varchar(36)  not null
        constraint constr_component_config_pk
            primary key,
    component_id varchar(36)  not null
        constraint fk_component_config
            references component,
    name         varchar(255) not null,
    value        varchar(4000)
);

alter table component_config
    owner to admin;

create index if not exists idx_compo_config_compo
    on component_config (component_id);

create index if not exists idx_component_realm
    on component (realm_id);

create index if not exists idx_component_provider_type
    on component (provider_type);

create table if not exists federated_user
(
    id                  varchar(255) not null
        constraint constr_federated_user
            primary key,
    storage_provider_id varchar(255),
    realm_id            varchar(36)  not null
);

alter table federated_user
    owner to admin;

create table if not exists client_initial_access
(
    id              varchar(36) not null
        constraint cnstr_client_init_acc_pk
            primary key,
    realm_id        varchar(36) not null
        constraint fk_client_init_acc_realm
            references realm,
    timestamp       integer,
    expiration      integer,
    count           integer,
    remaining_count integer
);

alter table client_initial_access
    owner to admin;

create index if not exists idx_client_init_acc_realm
    on client_initial_access (realm_id);

create table if not exists client_auth_flow_bindings
(
    client_id    varchar(36)  not null,
    flow_id      varchar(36),
    binding_name varchar(255) not null,
    constraint c_cli_flow_bind
        primary key (client_id, binding_name)
);

alter table client_auth_flow_bindings
    owner to admin;

create table if not exists client_scope_client
(
    client_id     varchar(255)          not null,
    scope_id      varchar(255)          not null,
    default_scope boolean default false not null,
    constraint c_cli_scope_bind
        primary key (client_id, scope_id)
);

alter table client_scope_client
    owner to admin;

create index if not exists idx_clscope_cl
    on client_scope_client (client_id);

create index if not exists idx_cl_clscope
    on client_scope_client (scope_id);

create table if not exists default_client_scope
(
    realm_id      varchar(36)           not null
        constraint fk_r_def_cli_scope_realm
            references realm,
    scope_id      varchar(36)           not null,
    default_scope boolean default false not null,
    constraint r_def_cli_scope_bind
        primary key (realm_id, scope_id)
);

alter table default_client_scope
    owner to admin;

create index if not exists idx_defcls_realm
    on default_client_scope (realm_id);

create index if not exists idx_defcls_scope
    on default_client_scope (scope_id);

create table if not exists user_consent_client_scope
(
    user_consent_id varchar(36) not null
        constraint fk_grntcsnt_clsc_usc
            references user_consent,
    scope_id        varchar(36) not null,
    constraint constraint_grntcsnt_clsc_pm
        primary key (user_consent_id, scope_id)
);

alter table user_consent_client_scope
    owner to admin;

create index if not exists idx_usconsent_clscope
    on user_consent_client_scope (user_consent_id);

create table if not exists fed_user_consent_cl_scope
(
    user_consent_id varchar(36) not null,
    scope_id        varchar(36) not null,
    constraint constraint_fgrntcsnt_clsc_pm
        primary key (user_consent_id, scope_id)
);

alter table fed_user_consent_cl_scope
    owner to admin;

create table if not exists resource_server_perm_ticket
(
    id                 varchar(36)  not null
        constraint constraint_fapmt
            primary key,
    owner              varchar(255) not null,
    requester          varchar(255) not null,
    created_timestamp  bigint       not null,
    granted_timestamp  bigint,
    resource_id        varchar(36)  not null
        constraint fk_frsrho213xcx4wnkog83sspmt
            references resource_server_resource,
    scope_id           varchar(36)
        constraint fk_frsrho213xcx4wnkog84sspmt
            references resource_server_scope,
    resource_server_id varchar(36)  not null
        constraint fk_frsrho213xcx4wnkog82sspmt
            references resource_server,
    policy_id          varchar(36)
        constraint fk_frsrpo2128cx4wnkog82ssrfy
            references resource_server_policy,
    constraint uk_frsr6t700s9v50bu18ws5pmt
        unique (owner, requester, resource_server_id, resource_id, scope_id)
);

alter table resource_server_perm_ticket
    owner to admin;

create table if not exists resource_attribute
(
    id          varchar(36) default 'sybase-needs-something-here'::character varying not null
        constraint res_attr_pk
            primary key,
    name        varchar(255)                                                         not null,
    value       varchar(255),
    resource_id varchar(36)                                                          not null
        constraint fk_5hrm2vlf9ql5fu022kqepovbr
            references resource_server_resource
);

alter table resource_attribute
    owner to admin;

create table if not exists resource_uris
(
    resource_id varchar(36)  not null
        constraint fk_resource_server_uris
            references resource_server_resource,
    value       varchar(255) not null,
    constraint constraint_resour_uris_pk
        primary key (resource_id, value)
);

alter table resource_uris
    owner to admin;

create table if not exists role_attribute
(
    id      varchar(36)  not null
        constraint constraint_role_attribute_pk
            primary key,
    role_id varchar(36)  not null
        constraint fk_role_attribute_id
            references keycloak_role,
    name    varchar(255) not null,
    value   varchar(255)
);

alter table role_attribute
    owner to admin;

create index if not exists idx_role_attribute
    on role_attribute (role_id);

create table if not exists realm_localizations
(
    realm_id varchar(255) not null,
    locale   varchar(255) not null,
    texts    text         not null,
    constraint realm_localizations_pkey
        primary key (realm_id, locale)
);

alter table realm_localizations
    owner to admin;
