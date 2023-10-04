// @ts-nocheck
// Note: type annotations allow type checking and IDEs autocompletion

const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');
const simplePlantUML = require('@akebifiky/remark-simple-plantuml');

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'Profio Application',
  tagline: '🚛 A Modern Logistics Management System ✈️',
  url: 'https://HutechCJ.github.io',
  baseUrl: '/ProfioApp',
  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',
  trailingSlash: false,
  favicon: 'img/favicon.ico',
  organizationName: 'HutechCJ',
  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      metadata: [
        {
          name: 'keywords',
          content:
            'CJ, Logistics, Profio, Application, Hutech, University, CJ Logistics, Hutech CJ',
        },
        {
          name: 'description',
          content: 'A Modern Logistics Management System',
        },
        { name: 'og:title', content: 'Profio Application' },
        {
          name: 'og:description',
          content: 'A Modern Logistics Management System',
        },
      ],
      zoom: {
        selector: '.markdown :not(em) > img',
        config: {
          background: {
            light: 'rgb(255, 255, 255)',
            dark: 'rgb(50, 50, 50)',
          },
        },
      },
      docs: {
        sidebar: {
          autoCollapseCategories: true,
        },
      },
      announcementBar: {
        id: 'announcement-bar',
        content:
          '<a target="_blank" rel="nofollow noopener noreferrer" href="https://github.com/HutechCJ/ProfioApp">⭐ Star Application on GitHub</a>',
        isCloseable: true,
      },
      image: 'img/web-preview.jpg',
      navbar: {
        title: 'Profio Application',
        logo: {
          alt: 'CJ Logo',
          src: 'img/logo.png',
        },
        items: [
          {
            type: 'doc',
            docId: 'intro',
            position: 'left',
            label: 'Documentation',
          },
          {
            href: 'https://www.cjlogistics.com/en/main',
            label: 'Website',
            position: 'left',
            target: '_self',
          },
          {
            href: 'https://github.com/HutechCJ/ProfioApp',
            className: 'header-github-link',
            'aria-label': 'GitHub repository',
            position: 'right',
          },
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: 'Company',
            items: [
              {
                label: 'About',
                href: 'https://www.cjlogistics.com/en/about/brand/message',
              },
              {
                label: 'Library',
                href: 'https://www.cjlogistics.com/en/about/brand/story',
              },
              {
                label: 'Sponsorship',
                href: 'https://www.cjlogistics.com/en/about/sponsorship/motorsports',
              },
              {
                label: 'Investor Relations',
                href: 'https://www.cjlogistics.com/en/page/investment/composition',
              },
              {
                label: 'Recruitment',
                href: 'https://www.cjlogistics.com/en/page/about/cj-way',
              },
              {
                html: `
                <a href="https://www.cjlogistics.com/en/main" target="_blank" rel="noreferrer noopener" aria-label="CJ Logistics">
                  <img src="/img/cj-banner.png" alt="CJ Logistics" width="150" height="55" />
                </a>
              `,
              },
            ],
          },
          {
            title: 'Legal',
            items: [
              {
                label: 'Privacy Policy',
                href: 'https://www.cjlogistics.com/en/agreement/privacy-policy',
              },
              {
                label: 'Operation Policy',
                href: 'https://www.cjlogistics.com/en/agreement/operation-management-policy',
              },
            ],
          },
          {
            title: 'Support',
            items: [
              {
                label: 'Business Inquiry',
                href: 'https://www.cjlogistics.com/en/support/contacts',
              },
              {
                label: 'FAQ',
                href: 'https://www.cjlogistics.com/en/support/faq',
              },
              {
                label: '1:1 Assistance',
                href: 'https://www.cjlogistics.com/en/support/inquiry/agreement',
              },
            ],
          },
          {
            title: 'More',
            items: [
              {
                label: 'CJ Vietnam',
                href: 'https://cjvietnam.vn/vi/about/overview',
              },
              {
                label: 'Hutech University',
                href: 'https://hutech.edu.vn/',
              },
              {
                label: 'Hutech CJ',
                href: 'https://github.com/HutechCJ',
              },
            ],
          },
        ],
        copyright: `Copyright © ${new Date().getFullYear()} HutechCJ. All rights reserved`,
        logo: {
          alt: 'School Logo',
          src: 'img/school.png',
          href: 'https://github.com/HutechCJ',
          width: 300,
        },
      },
      prism: {
        theme: lightCodeTheme,
        darkTheme: darkCodeTheme,
      },
      colorMode: {
        defaultMode: 'light',
        disableSwitch: true,
        respectPrefersColorScheme: false,
      },
    }),
  presets: [
    [
      'classic',
      /** @type {import('@docusaurus/preset-classic').Options} */
      ({
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),
          editUrl: 'https://github.com/HutechCJ/ProfioApp/blob/main/apps/docs/',
          remarkPlugins: [simplePlantUML],
          showLastUpdateAuthor: true,
          showLastUpdateTime: true,
          lastVersion: 'current',
        },
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
      }),
    ],
  ],
  plugins: [
    [
      '@docusaurus/plugin-pwa',
      {
        offlineModeActivationStrategies: [
          'appInstalled',
          'standalone',
          'queryString',
        ],
        pwaHead: [
          {
            tagName: 'link',
            rel: 'icon',
            href: '/img/logo.png',
          },
          {
            tagName: 'link',
            rel: 'manifest',
            href: '/manifest.json',
          },
          {
            tagName: 'meta',
            name: 'theme-color',
            content: 'rgb(37, 194, 160)',
          },
        ],
      },
    ],
  ],
};

module.exports = config;
